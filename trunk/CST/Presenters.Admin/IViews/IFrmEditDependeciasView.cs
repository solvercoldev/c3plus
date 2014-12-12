using System;
using Application.Core;

namespace Presenters.Admin.IViews
{
    public interface IFrmEditDependeciasView : IView
    {
        #region Events

        event EventHandler SaveEvent;
        event EventHandler ActualizarEvent;

        #endregion

        #region Members

        bool Activo { get; set; }
        string Descripcion { get; set; }
        string IdDependencia { get; set; }
        string CreatedBy { set; }
        string CreatedOn { set; }
        string ModifiedBy { set; }
        string ModifiedOn { set; }

        #endregion
    }
}