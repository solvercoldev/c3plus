using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;

namespace Domain.MainModule.Contratos.Contracts
{
    public interface ILocalizacionesRepository : IRepository<Localizaciones>
    {
        Localizaciones GetCompleteEntity(ISpecification<Localizaciones> specification);
    }
}
    