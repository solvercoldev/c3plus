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
        string CreatedBy { set; }
        string CreatedOn { set; }
        string ModifiedBy { set; }
        string ModifiedOn { set; }

        #endregion
    }
}