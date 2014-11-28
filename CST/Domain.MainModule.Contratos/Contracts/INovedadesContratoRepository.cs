using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Domain.MainModule.Contratos.Contracts
{
    public interface INovedadesContratoRepository : IRepository<NovedadesContrato>
    {
        List<NovedadesContrato> GetCompleteEntityList(ISpecification<NovedadesContrato> specification);
        NovedadesContrato GetCompleteEntity(ISpecification<NovedadesContrato> specification);
    }
}
    