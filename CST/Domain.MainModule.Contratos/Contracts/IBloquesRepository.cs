using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;

namespace Domain.MainModule.Contratos.Contracts
{
    public interface IBloquesRepository : IRepository<Bloques>
    {
        Bloques GetCompleteEntity(ISpecification<Bloques> specification);
    }
}
    