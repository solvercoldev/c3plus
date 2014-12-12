using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Contratos.IViews
{
    public interface INewRadicadoContratoView : IView
    {
        string IdContrato { get; }
        string IdRadicado { get; }    

        string TipoRadicado { get; set; }
        string Numero { get; set; }
        DateTime FechaRadicado { get; set; }
        string Asunto { get; set; }
        int DirigidoA { get; set; }
        int IdEnviadoPor { get; set; }
        string EnviadoPor { get; set; }
        string DirigidoAExterno { get; set; }
        string Resumen { get; set; }
        bool RespondeRE { get; set; }
        long IdRadicadoEntradaAsociado { get; set; }
        bool RespuestaPendiente { get; set; }
        string NombreAnexo { get; }
        byte[] ArchivoAnexo { get; }

        int ResponsableRespuesta { get; set; }
        DateTime FechaRespuesta { get; set; }
        int DiasAlarma { get; set; }

        DTO_ValueKey ArchivoAdjunto { get; set; }

        void LoadUsuarios(List<TBL_Admin_Usuarios> items);
        void LoadRadicadosPendientes(List<Radicados> items);
        void GoToContratoView();
        void GoToRadicadoView(long idRadicado);

        void ShowRespondeRadicadoSalida(bool visible);
        void ShowRespuestaRadicado(bool visible);
        void ShowRadicadoSalida(bool visible);
    }
}