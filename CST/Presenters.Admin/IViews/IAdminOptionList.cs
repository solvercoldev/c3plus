using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Admin.IViews
{
    public interface IAdminOptionList : IView
    {
        event EventHandler FilterEvent;
        event EventHandler PagerEvent;

        void GetOptionsList(List<TBL_Admin_OptionList> items);

        int TotalRegistrosPaginador { set; }

        int PageZise { get; }

        string IdModule { get; }

        string Search { get; }
    }
}
