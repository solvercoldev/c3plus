using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Domain.MainModule.Contratos.Contracts
{
    public interface ICompromisosRepository : IRepository<Compromisos>
    {
        Compromisos GetCompleteEntity(ISpecification<Compromisos> specification);
        List<Compromisos> GetCompleteEntityList(ISpecification<Compromisos> specification);
        Compromisos GetWitFase(ISpecification<Compromisos> specification);
    }
}