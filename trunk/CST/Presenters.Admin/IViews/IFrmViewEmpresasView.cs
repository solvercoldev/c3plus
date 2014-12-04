using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Admin.IViews
{
    public interface IFrmViewEmpresasView : IView
    {
        event EventHandler FilterEvent;

        int TotalRegistrosPaginador { set; }

        int PageZise { get; }

        string ModuleSetupId { get; set; }

        void GetEmpresas(List<Empresas> items);
    }
}