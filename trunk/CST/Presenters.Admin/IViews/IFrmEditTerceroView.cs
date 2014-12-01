using System;
using Application.Core;

namespace Presenters.Admin.IViews
{
    public interface IFrmEditTerceroView : IView
    {
        #region Events

        event EventHandler SaveEvent;

        event EventHandler DeleteEvent;

        event EventHandler ActualizarEvent;

        #endregion

        #region Members

        string Nombre { get; set; }
        string IdTercero { get; set; }

        #endregion
    
    }
}