using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;

namespace Application.MainModule.Contratos.IServices
{
    public interface ISfTiposContratoManagementServices : IGenericServices<TiposContrato>
    {
        TiposContrato GetById(string id);
        int CountByPaged();
    }
}
    