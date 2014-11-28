using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Domain.MainModule.Contratos.Contracts
{
    public interface INovedadesFaseRepository : IRepository<NovedadesFase>
    {
        NovedadesFase GetCompleteEntity(ISpecification<NovedadesFase> specification);
        List<NovedadesFase> GetCompleteEntityList(ISpecification<NovedadesFase> specification);
    }
}
    