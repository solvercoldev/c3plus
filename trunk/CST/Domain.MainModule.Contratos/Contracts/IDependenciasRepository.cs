using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;

namespace Domain.MainModule.Contratos.Contracts
{
    public interface IDependenciasRepository : IRepository<Dependencias>
    {
        Dependencias GetCompleteEntity(Specification<Dependencias> specification);
    }
}
    