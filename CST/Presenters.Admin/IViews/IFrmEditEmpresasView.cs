using System;
using Application.Core;

namespace Presenters.Admin.IViews
{
    public interface IFrmEditEmpresasView : IView
    {
        #region Events

        event EventHandler SaveEvent;
        event EventHandler DeleteEvent;
        event EventHandler ActualizarEvent;

        #endregion

        #region Members

        bool Activo { get; set; }
        string Nit { get; set; }
        string RazonSocial { get; set;}
        string Direccion { get; set; }
        string Telefono1 { get; set; }
        string Telefono2 { get; set; }
        string Logo { get; set; }
        string CreatedBy { set; }
        string CreatedOn { set; }
        string ModifiedBy { set; }
        string ModifiedOn { set; }

        #endregion
    }
}