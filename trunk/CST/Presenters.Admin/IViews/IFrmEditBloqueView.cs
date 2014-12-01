using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Admin.IViews
{
    public interface IFrmEditBloqueView : IView
    {
        #region Events

        event EventHandler SaveEvent;

        event EventHandler DeleteEvent;

        event EventHandler ActualizarEvent;

        #endregion

        #region Members

        bool Activo { get; set; }
        string Descripcion { get; set; }
        string IdBloque { get; set; }
        string CreatedBy { get; set; }
        string CreatedOn { get; set; }
        string ModifiedBy { get; set; }
        string ModifiedOn { get; set; }

        #endregion
    
    }
}