using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Domain.MainModule.Contratos.Contracts
{
    public interface IRadicadosRepository : IRepository<Radicados>
    {
        Radicados GetCompleteEntity(ISpecification<Radicados> specification);
        List<Radicados> GetCompleteEntityList(ISpecification<Radicados> specification);
    }
}