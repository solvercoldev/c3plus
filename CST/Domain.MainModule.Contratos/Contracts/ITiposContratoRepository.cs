using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;

namespace Domain.MainModule.Contratos.Contracts
{
    public interface ITiposContratoRepository : IRepository<TiposContrato>
    {
        TiposContrato GetCompleteEntity(ISpecification<TiposContrato> specification);
    }
}
    