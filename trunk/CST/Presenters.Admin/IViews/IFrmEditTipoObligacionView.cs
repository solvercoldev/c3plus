using System;
using Application.Core;

namespace Presenters.Admin.IViews
{
    public interface IFrmEditTipoObligacionView : IView
    {
        #region Events

        event EventHandler SaveEvent;

        event EventHandler DeleteEvent;

        event EventHandler ActualizarEvent;

        #endregion

        #region Members

        string Descripcion { get; set; }
        string IdTipoPagoObligacion { get; set; }

        #endregion
    }
}