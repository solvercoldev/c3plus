using System.Linq.Expressions;

namespace Infraestructure.Data.Core.Extensions
{
    public static class ExpressionExtensions
    {
        /// <summary>
        /// RemoveConvert extension method. This method remove all "Convert" elements in
        /// a expression
        /// </summary>
        /// <param name="expression">The expression to remove convert</param>
        /// <returns>Expression with removed "Convert"</returns>
        public static Expression RemoveConvert(this Expression expression)
        {
            while ((expression != null) && ((expression.NodeType == ExpressionType.Convert) || (expression.NodeType == ExpressionType.ConvertChecked)))
            {
                expression = ((UnaryExpression)expression).Operand.RemoveConvert();
            }
            return expression;
        }
    }
}