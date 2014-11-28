using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Contratos.IViews
{
    public interface IAdminComentariosContratoView : IView
    {
        #region  Admin Comentario

        string IdContrato { get; }

        string Asunto { get; set; }
        string Comentario { get; set; }
        string IdUsuarioDestino { get; set; }
        List<DTO_ValueKey> ArchivosAdjuntos { get; set; }
        List<DTO_ValueKey> UsuariosCopia { get; set; }

        #endregion

        #region Methods

        void ShowAdminComentarioWindow(bool visible);
        void LoadComentarios(List<ComentariosRespuesta> items);
        void LoadDestinatarios(List<TBL_Admin_Usuarios> items);        
        void LoadArchivosAdjuntos(List<DTO_ValueKey> items);
        void LoadUsuariosCopia(List<DTO_ValueKey> items);
        void LoadUsuarioCopia(List<TBL_Admin_Usuarios> items);

        #endregion
    }
}
