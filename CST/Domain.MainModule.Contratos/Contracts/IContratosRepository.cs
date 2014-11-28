using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Domain.MainModule.Contratos.Contracts
{
    public interface IContratosRepository : IRepository<Domain.MainModules.Entities.Contratos>
    {
        Domain.MainModules.Entities.Contratos GetCompleteEntity(ISpecification<Domain.MainModules.Entities.Contratos> specification);
        List<Domain.MainModules.Entities.Contratos> GetCompleteEntityList(ISpecification<Domain.MainModules.Entities.Contratos> specification);
    }
}
    