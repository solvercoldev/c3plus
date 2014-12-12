using System;
using Application.Core;

namespace Presenters.Admin.IViews
{
    public interface IFrmEditMonedasView : IView
    {
        #region Events

        event EventHandler SaveEvent;
        event EventHandler ActualizarEvent;

        #endregion

        #region Members

        string Nombre { get; set; }
        string IdMoneda { get; set; }

        #endregion
    }
}