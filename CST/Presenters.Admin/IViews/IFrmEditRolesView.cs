using System;
using Application.Core;

namespace Presenters.Admin.IViews
{
    public interface IFrmEditRolesView : IView
    {
        #region Events

        event EventHandler SaveEvent;
        event EventHandler DeleteEvent;
        event EventHandler ActualizarEvent;

        #endregion

        #region Members

        bool Activo { get; set; }
        bool Grupo { get; set; }
        string NombreRol { get; set; }
        string IdRol { get; set; }
        string CreatedBy { set; }
        string CreatedOn { set; }
        string ModifiedBy { set; }
        string ModifiedOn { set; }

        #endregion
    }
}