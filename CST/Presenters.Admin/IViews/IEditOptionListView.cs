using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;
using System.Collections;

namespace Presenters.Admin.IViews
{
    public interface IEditOptionListView : IView
    {
        #region Events

        event EventHandler SaveEvent;
        event EventHandler DeleteEvent;

        #endregion

        #region Members
        string IdOpcion { get; }
        string IdModulo { set; }
        string key { set; }
        string value { get; set; }
        string descripcion { get; set; }
        bool Activo { get; set; }
        string CreateBy { set; }
        string CreateOn { set; }
        string ModifiedBy { set; }
        string ModifiedOn { set; }
        string IdModule { get; }
        #endregion
    }
}
