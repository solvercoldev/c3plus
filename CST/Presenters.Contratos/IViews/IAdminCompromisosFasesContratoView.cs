using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Contratos.IViews
{
    public interface IAdminCompromisosFasesContratoView : IView
    {
        string IdContrato { get; }

        string Nombre { get; set; }
        string Descripcion { get; set; }
        DateTime FechaCumplimiento { get; set; }
        int IdResponsable { get; set; }
        int IdFase { get; set; }

        bool CanAddCompromisos { get; set; }

        void LoadCompromisos(List<Compromisos> items);
        void LoadFases(List<Fases> items);
        void LoadResponsables(List<TBL_Admin_Usuarios> items);
        void ShowAdminWindow(bool visible);
    }
}