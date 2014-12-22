using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;

namespace Application.MainModule.Contratos.IServices
{
    public interface ISfEmpresasManagementServices : IGenericServices<Empresas>
    {
        Empresas GetById(string id);
        int CountByPaged();
    }
}
    