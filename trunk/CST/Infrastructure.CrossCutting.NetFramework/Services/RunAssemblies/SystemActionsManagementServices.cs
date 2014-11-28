using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.CrossCutting.NetFramework.Services.RunAssemblies
{
    public class SystemActionsManagementServices : ISystemActionsManagementServices
    {

        public string AssemblyQualifiedName { get; set; }

        public string MethodName { get; set; }

        public object[] Params { get; set; }

        public List<string> GetListErrors { get; set; }

        public object Execute()
        {
            try
            {

                var oType = Type.GetType(AssemblyQualifiedName);

                var magicConstructor = oType.GetConstructor(Type.EmptyTypes);

                var magicClassObject = magicConstructor.Invoke(new object[] { });

                if (magicClassObject != null)
                {
                    var magicMethod = oType.GetMethod(MethodName);
                    var magicValue = magicMethod.Invoke(magicClassObject, Params);

                    var p = oType.GetProperties().Where(x => x.Name == "GetListErrors").SingleOrDefault();
                    if (p != null)
                    {
                        GetListErrors = (List<string>)oType.GetProperty("GetListErrors").GetValue(magicClassObject, null);
                    }
                    return magicValue;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la ejecución del Metodo.", ex);
            }

            return null;
        }

        /// <summary>
        /// Ejecuta en Memoria la expresion pasadda como parámetro.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public object Execute(string expression)
        {
            if (string.IsNullOrEmpty(expression)) return null;

            var c = CodeDomProvider.CreateProvider("CSharp");
            var cp = new CompilerParameters();
            cp.ReferencedAssemblies.Add("system.dll");
            cp.CompilerOptions = "/t:library";
            cp.GenerateInMemory = true;

            var sb = new StringBuilder("");
            sb.Append("using System;\n");
            sb.Append("namespace Applications.MainModule.SystemActions.Service { \n");
            sb.Append("     public class EjecucionExpresion{ \n");
            sb.Append("         public string Evaluate(){\n");
            sb.Append("             return " + expression + ";\n");
            sb.Append("             } \n");
            sb.Append("     } \n");
            sb.Append("} \n");
            var cr = c.CompileAssemblyFromSource(cp, sb.ToString());
            if (cr.Errors.Count > 0)
            {
                var msgError = string.Format("ERROR: {0}  Error evaluating cs code ", cr.Errors[0].ErrorText);
                var lista = new List<string> { msgError };
                GetListErrors = lista;
                return "Error";
            }

            var a = cr.CompiledAssembly;
            var o = a.CreateInstance("Applications.MainModule.SystemActions.Service.EjecucionExpresion");
            if (o != null)
            {
                var t = o.GetType();
                var mi = t.GetMethod("Evaluate");
                var s = mi.Invoke(o, null);
                return s;
            }
            return null;
        }

    }
}