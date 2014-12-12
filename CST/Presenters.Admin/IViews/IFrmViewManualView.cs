using System;
using System.Collections.Generic;
using Domain.MainModules.Entities;

namespace Presenters.Admin.IViews
{
    public interface IFrmViewManualView
    {
        event EventHandler FilterEvent;

        int TotalRegistrosPaginador { set; }

        int PageZise { get; }

        string ModuleSetupId { get; set; }

        void GetEmpresas(List<ManualAnh> items);
    }
}