using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Domain.MainModule.Contratos.Contracts
{
    public interface IEntregablesANHCompromisoRepository : IRepository<EntregablesANHCompromiso>
    {
        List<EntregablesANHCompromiso> GetCompleteEntityList(ISpecification<EntregablesANHCompromiso> specification);
    }
}
    