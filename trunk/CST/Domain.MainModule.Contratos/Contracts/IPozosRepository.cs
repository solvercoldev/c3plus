using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;

namespace Domain.MainModule.Contratos.Contracts
{
    public interface IPozosRepository : IRepository<Pozos>
    {
        Pozos GetCompleteEntity(ISpecification<Pozos> specification);
    }
}
    