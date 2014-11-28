using System;
using System.Data.Linq;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Infraestructure.Data.Core.Extensions
{
    public static class TableExtensions
    {
        public static int BulkDelete<TEntity>(this IQueryable<TEntity> queryable, Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {

            var query = queryable as ObjectQuery<TEntity>;

            var tableName = typeof(TEntity).Name;
            
            var command = String.Format("DELETE FROM {0}", tableName);

            var conditionBuilder = new ConditionBuilder();

            conditionBuilder.Build(predicate.Body);

            if (!String.IsNullOrEmpty(conditionBuilder.Condition))
            {
                command += " WHERE " + conditionBuilder.Condition;
            }

            return query.Context.ExecuteStoreCommand(command, conditionBuilder.Arguments);
        }

        public static int BulkUpdate<TEntity>(this IQueryable<TEntity> queryable,
            Expression<Func<TEntity, TEntity>> evaluator, Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            var query = queryable as ObjectQuery<TEntity>;
            var tableName = typeof(TEntity).Name;

            var command = String.Format("UPDATE  {0} ", tableName);

            if(evaluator != null)
            {
               

                var initExpression = evaluator.Body as MemberInitExpression;
                if(initExpression != null)
                {
                    var result = (from m in initExpression.Bindings.OfType<MemberAssignment>()
                                  select new { PropertyName = m.Member.Name, Value =  m.Expression.ToString(), Tipo = m.Expression.Type.Name }).ToList();

                    var sb = new StringBuilder();
                    sb.Append("SET ");
                   

                    for (var i = 0; i < result.Count; i++)
                    {
                        if( i>0)
                            sb.Append(",");

                        sb.Append(result[i].PropertyName);
                        sb.Append("=");

                        object res;
                        switch (result[i].Tipo)
                        {
                            case "Int32":
                                res = GetValueProperty(evaluator, result[i].PropertyName);
                                sb.Append(Convert.ToInt32(res));
                                break;
                            case "String":
                                res = GetValueProperty(evaluator, result[i].PropertyName);
                                sb.Append(string.Format("'{0}'", res));
                                break;
                            case "Decimal":
                                 res = GetValueProperty(evaluator, result[i].PropertyName);
                                 sb.Append(res.ToString().Replace(",","."));
                                break;
                            case "Boolean":
                                 res = GetValueProperty(evaluator, result[i].PropertyName);
                                 sb.Append(Convert.ToBoolean(res));
                                break;
                        }

                        //sb.Append(GetValueProperty(evaluator, result[i].PropertyName));
                    }
                    command += sb.ToString();
                }

            }

            var conditionBuilder = new ConditionBuilder();
            conditionBuilder.Build(predicate.Body);
            if (!String.IsNullOrEmpty(conditionBuilder.Condition))
            {
                command += " WHERE " + conditionBuilder.Condition;
            }
           
            return query.Context.ExecuteStoreCommand(command, conditionBuilder.Arguments);
        }

        private static object GetValueProperty<TEntity>(Expression<Func<TEntity, TEntity>> evaluator, string property)
           where TEntity : class
        {

            var setValue = new ConditionBuilder();
            setValue.Build(evaluator.Body);
            if (setValue.Arguments.Count() <= 0) return null;
          
            var objeto = setValue.Arguments[0];
            object resultado = null;   


            foreach (var p in objeto.GetType().GetProperties())
            {

                var valor = p.GetValue(objeto, null);

                if (p.Name != property) continue;
                resultado = valor;
                break;

                //switch (p.PropertyType.Name)
                //{
                //    case "Int32":
                //        resultado = valor;
                //        break;
                //    case "String":
                //        resultado = string.Format("'{0}'",valor);
                //        break;
                //    case "Decimal":
                //        resultado= valor;
                //        break;
                //    case "Boolean":
                //        resultado = valor;
                //        break;
                //}
            }
            return resultado;
        }

    }
}