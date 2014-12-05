using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Contratos.IViews
{
    public interface IAdminNovedadesContratoView : IView
    {
        string IdContrato { get; }

        string TipoOperacion { get; set; }
        
        string Descripcion { get; set; }
        DateTime FechaNovedad { get; set; }
        DateTime FechaFinNovedad { get; set; }

        DateTime FechaFirma { set; }

        void LoadNovedades(List<NovedadesContrato> items);
        void ShowAdminWindow(bool visible);

        bool CanSuspender { get; set; }
        bool CanRestituir { get; set; }
        bool CanRenunciar { get; set; }
        bool CanTerminar { get; set; }
        bool CanAnular { get; set; }
    }
}