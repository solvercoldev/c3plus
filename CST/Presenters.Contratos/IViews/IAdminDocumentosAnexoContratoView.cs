using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Contratos.IViews
{
    public interface IAdminDocumentosAnexoContratoView : IView
    {
        string IdContrato { get; }

        string Categoria { get; set; }
        string CategoriaDocumento { get; set; }
        string Titulo { get; set; }
        string Descripcion { get; set; }
        string NombreArchivo { get; }
        byte[] ArchivoAnexo { get; }
        void LoadCategorias(List<DTO_ValueKey> items);
        void LoadAnexos(List<DocumentosAnexoContrato> items);

        bool CanAddDocumentos { get; set; }
    }
}