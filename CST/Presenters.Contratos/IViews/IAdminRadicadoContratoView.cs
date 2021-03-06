﻿using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Contratos.IViews
{
    public interface IAdminRadicadoContratoView : IView
    {
        string IdContrato { get; }
        string IdRadicado { get; }

        string InfoContrato { get; set; }
        string Numero { get; set; }
        string Estado { get; set; }
        string Responsable { get; set; }
        string FechaVencimiento { get; set; }
        string FechaCreacion { get; set; }
        string Asunto { get; set; }
        string EnviadoPor { get; set; }
        string DirigidoA { get; set; }
        string Descripcion { get; set; }
        string REAsociado { get; set; }

        string ResponsableRespuesta { get; set; }
        string FechaRespuesta { get; set; }
        string FechaAlarmaRespuesta { get; set; }

        string TipoOperacion { get; set; }

        string ObservacionesNovedad { get; set; }

        DateTime FechaReprogramacion { get; set; }

        DTO_ValueKey ArchivoAdjunto { get; set; }

        void LoadResponsables(List<TBL_Admin_Usuarios> items);

        int ResponsableReprogramacion { get; set; }

        void ShowRespuesta(bool visible);
        void ShowREAsociado(bool visible);

        void EnableMarcarOK(bool enable);

        void EnableEdit(bool enable);

        void EnableAnular(bool enable);

        void EnableReprogramar(bool enable);

        void EnableReasignar(bool enable);

        void EnableActions(bool enable);

        void EnableResponsableVence(bool enable);

        string MsgLogInfo { get; set; }
    }
}