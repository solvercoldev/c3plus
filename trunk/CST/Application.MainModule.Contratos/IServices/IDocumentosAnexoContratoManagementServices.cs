using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using System.Collections.Generic;
using System;

namespace Application.MainModule.Contratos.IServices
{
    public interface ISfDocumentosAnexoContratoManagementServices : IGenericServices<DocumentosAnexoContrato>
    {
        DocumentosAnexoContrato GetById(Guid id);
        List<DocumentosAnexoContrato> GetAnexosByContratoCategoria(int idContrato, string categoria);
    }
}