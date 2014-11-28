using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Contratos.IViews
{
    public interface IAdminComentarioRespuestaView : IView
    {
        // Seccion Info Contrato
        string IdContrato { get; set; }

        string NombreContrato { get; set; }
        string NumeroContrato { get; set; }
        string Empresa { get; set; }
        string Bloque { get; set; }
        string FechaFirma { get; set; }
        string Periodo { get; set; }

        // Admin Comentario Respuesta
        string IdComentario { get; }
        string Asunto { get; set; }
        string Mensaje { get; set; }
        string Destinatario { get; set; }
        DateTime FechaComentario { get; set; }
        string NuevoComentario { get; set; }
        string IdUsuarioDestino { get; set; }
        void LoadUsuariosCopia(List<DTO_ValueKey> items);

        // Methods
        void EnableEdit(bool enabled);
        void LoadDestinatarios(List<TBL_Admin_Usuarios> items);
        void LoadComentariosRelacionados(List<ComentariosRespuesta> items);

        // Archivos Adjuntos        
        byte[] ArchivoAdjunto { get; }
        string NombreArchivoAdjunto { get; }
        void LoadArchivosAdjuntos(List<DTO_ValueKey> items);
        void DescargarArchivo(DTO_ValueKey archivo);
    }
}