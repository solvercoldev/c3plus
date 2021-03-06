using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;

namespace Application.MainModule.Contratos.IServices
{
    public interface ISfDependenciasManagementServices : IGenericServices<Dependencias>
    {
        Dependencias GetById(string id);
        int CountByPaged();
    }
}
    