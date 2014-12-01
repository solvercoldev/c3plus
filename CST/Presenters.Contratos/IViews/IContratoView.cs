using System.Collections.Generic;
using Application.Core;
using Application.MainModule.Contratos.DTO;
using Domain.MainModules.Entities;

namespace Presenters.Contratos.IViews
{
    public interface IContratoView : IView
    {
        string IdContrato { get; }

        string NombreContrato { get; set; }
        string EstadoContrato { get; set; }
        string NumeroContrato { get; set; }
        string Empresa { get; set; }
        string Bloque { get; set; }
        string TipoContrato { get; set; }
        string FechaFirma { get; set; }
        string FechaEfectiva { get; set; }
        string Periodo { get; set; }
        string ImagenContrato { set; }
        string MsgLogInfo { get; set; }

        bool CanTrabajarFases { get; set; }

        void LoadFases(List<Fases> items);

        // Load
        void LoadSecciones(IEnumerable<TBL_Admin_Secciones> secciones);
        void LoadInitControl();
    }
}
