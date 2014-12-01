using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Contratos.IViews
{
    public interface IAdminRadicadosContratoView : IView
    {
        string IdContrato { get; }

        string TipoRadicado { get; set; }
        string EstadoRadicado { get; set; }
        string SearchText { get; set; }

        void LoadRadicados(List<Radicados> items);
    }
}