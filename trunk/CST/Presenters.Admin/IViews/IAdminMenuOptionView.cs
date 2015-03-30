using System;
using System.Collections;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Admin.IViews
{
    public interface IAdminMenuOptionView : IView
    {

        #region Events

        /// <summary>
        /// Evento cuando el usuario guarda un registro en la BD
        /// </summary>
        event EventHandler SaveEvent;

        /// <summary>
        /// Evento cuando el usuario elimina un registro en BD
        /// </summary>
        event EventHandler DeleteEvent;

        /// <summary>
        /// Evento que permite cargar el detalle asociado al nodo seleccionado
        /// </summary>
        event EventHandler LoadDetalleEvent;

        #endregion

        #region Members

        void GetAllRoles(IEnumerable<TBL_Admin_Roles> items);
        void RolesAsigandos(IEnumerable<TBL_Admin_Roles> items);
        void OpcionesMenu(IEnumerable<TBL_Admin_OpcionesMenu> items);
        string Descripcion { get; set; }
        string Ulr { get; set; }
        string Posicion { get; set; }
        bool Activo { get; set; }
        bool ShowInMainMenu { get; set; }
        bool ShowInSecondMenu { get; set; }
        int? IdOpcionMenu { get; set; }
        int AplicationId { get; set; }
        ArrayList GetSelectdRole();

        #endregion
    }

}