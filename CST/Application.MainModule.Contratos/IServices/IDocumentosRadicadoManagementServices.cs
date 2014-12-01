using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System;

namespace Application.MainModule.Contratos.IServices
{
    public interface ISfDocumentosRadicadoManagementServices : IGenericServices<DocumentosRadicado>
    {
        DocumentosRadicado GetById(Guid id);
        DocumentosRadicado GetByIdRadicado(long idRadicado);
    }
}
    