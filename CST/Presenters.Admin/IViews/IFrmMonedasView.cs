using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Admin.IViews
{
    public interface IFrmMonedasView : IView
    {
        event EventHandler FilterEvent;

        int TotalRegistrosPaginador { set; }

        int PageZise { get; }

        string ModuleSetupId { get; set; }

        void GetMonedas(List<Monedas> items);
    }
}