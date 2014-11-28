using System.Collections.Generic;

namespace Infrastructure.CrossCutting.NetFramework.Services.RunAssemblies
{
    public interface ISystemActionsManagementServices
    {
        string AssemblyQualifiedName { get; set; }

        string MethodName { get; set; }

        object[] Params { get; set; }

        object Execute();

        List<string> GetListErrors { get; set; }

        object Execute(string expression);
    }
}