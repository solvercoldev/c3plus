using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;

namespace Application.MainModule.Contratos.IServices
{
    public interface ISfTiposPagoObligacionManagementServices : IGenericServices<TiposPagoObligacion>
    {
        TiposPagoObligacion GetById(string id);
        int CountByPaged();
    }
}
    