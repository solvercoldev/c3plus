using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;

namespace Domain.MainModule.Contratos.Contracts
{
    public interface ICamposRepository : IRepository<Campos>
    {
        Campos GetCompleteEntity(ISpecification<Campos> specification);
    }
}
    