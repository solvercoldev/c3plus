using System.Collections.Generic;
using Application.Core;
using Application.MainModule.Contratos.DTO;
using Domain.MainModules.Entities;
using System;

namespace Presenters.Contratos.IViews
{
    public interface IManageFasesContratoView : IView
    {
        string IdContrato { get; }

        string NombreContrato { get; set; }


        string TipoOperacion { get; set; }

        int IdFase { get; set; }
        string Descripcion { get; set; }
        DateTime FechaFinalExtension { get; set; }

        void LoadFases(List<Fases> items);
        // Load
        void LoadSecciones(IEnumerable<TBL_Admin_Secciones> secciones);
        void LoadInitControl();
    }
}