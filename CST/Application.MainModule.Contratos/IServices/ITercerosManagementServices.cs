using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;

namespace Application.MainModule.Contratos.IServices
{
    public interface ISfTercerosManagementServices : IGenericServices<Terceros>
    {
        Terceros GetById(string id);
        int CountByPaged();
    }
}
    