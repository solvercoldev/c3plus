using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;
using System.Collections;

namespace Presenters.Admin.IViews
{
    public interface IDetailOptionListView : IView
    {
        #region Members
        string IdOpcion { get; }
        string IdModulo { set; }
        string key { set; }
        string value { set; }
        string descripcion { set; }
        bool Activo { set; }
        string CreateBy { set; }
        string CreateOn { set; }
        string ModifiedBy { set; }
        string ModifiedOn { set; }
        string IdModule { get; }

        #endregion
    }
}
