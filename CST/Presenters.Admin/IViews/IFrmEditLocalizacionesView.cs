using System;
using Application.Core;

namespace Presenters.Admin.IViews
{
    public interface IFrmEditLocalizacionesView : IView
    {
        #region Events

        event EventHandler SaveEvent;
        event EventHandler ActualizarEvent;

        #endregion

        #region Members

        bool Activo { get; set; }
        string Descripcion { get; set; }
        string IdLocalizacion { get; set; }
        string CreatedBy { set; }
        string CreatedOn { set; }
        string ModifiedBy { set; }
        string ModifiedOn { set; }

        #endregion
    }
}