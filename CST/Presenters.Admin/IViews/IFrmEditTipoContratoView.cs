using System;
using Application.Core;

namespace Presenters.Admin.IViews
{
    public interface IFrmEditTipoContratoView : IView
    {
        #region Events

        event EventHandler SaveEvent;

        event EventHandler DeleteEvent;

        event EventHandler ActualizarEvent;

        #endregion

        #region Members

        bool Activo { get; set; }
        string Descripcion { get; set; }
        string IdTipoContrato { get; set; }
        string CreatedBy { get; set; }
        string CreatedOn { get; set; }
        string ModifiedBy { get; set; }
        string ModifiedOn { get; set; }

        #endregion
    }
}