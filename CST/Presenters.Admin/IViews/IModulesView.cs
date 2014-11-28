using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Admin.IViews
{
    public interface IModulesView : IView
    {

        event EventHandler FilterEvent;

        event EventHandler UpdateEvent;

        void GetModules(List<TBL_Admin_ModuleType> modules);

        int PageZise { get; }

        int TotalRegistrosPaginador { set; }
    }
}