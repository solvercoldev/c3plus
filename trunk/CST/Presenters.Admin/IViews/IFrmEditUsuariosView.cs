using System;
using System.Collections;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Admin.IViews
{
    public interface IFrmEditUsuariosView : IView
    {
        #region Events

        event EventHandler SaveEvent;
        event EventHandler ActualizarEvent;

        #endregion

        #region Members

        void ListadoDependencias(List<Dependencias> items);
        void ListadoLocalizacion(List<Localizaciones> items);
        void GetAllRoles(IList<TBL_Admin_Roles> items);
        void RolesAsigandos(IList<TBL_Admin_Roles> items);
        ArrayList GetSelectdRole();
        bool Activo { get; set; }
        string IdUser { get; set; }
        string CodigoUser { get; set; }
        string Nombres { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string Email { get; set; }
        string TelefonoFijo { get; set; }
        string Documento { get; set; }
        string Movil { get; set; }
        string IdLocalizacion { get; set; }
        string Direccion { get; set; }
        string Extension { get; set; }
        string IdDependencia { get; set; }
        string Cargo { get; set; }
        string CreatedBy { set; }
        string CreatedOn { set; }
        string ModifiedBy { set; }
        string ModifiedOn { set; }

        #endregion
    }
}