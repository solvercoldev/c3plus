using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;

namespace Domain.MainModule.Contratos.Contracts
{
    public interface IDocumentosAnexoContratoRepository : IRepository<DocumentosAnexoContrato>
    {
        DocumentosAnexoContrato GetCompleteEntity(ISpecification<DocumentosAnexoContrato> specification);
        List<DocumentosAnexoContrato> GetCompleteEntityList(ISpecification<DocumentosAnexoContrato> specification);
    }
}