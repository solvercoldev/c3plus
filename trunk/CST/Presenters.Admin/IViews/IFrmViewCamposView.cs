using System;
using System.Collections.Generic;
using Domain.MainModules.Entities;

namespace Presenters.Admin.IViews
{
    public interface IFrmViewCamposView
    {
        event EventHandler FilterEvent;

        int TotalRegistrosPaginador { set; }

        int PageZise { get; }

        string ModuleSetupId { get; set; }

        void GetCampos(List<Campos> items);
    }
}