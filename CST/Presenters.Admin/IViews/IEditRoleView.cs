using System;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Admin.IViews
{
    public interface IEditRoleView : IView
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

        #endregion

        #region Members
        string RoleId { get; }
        string Name { get; set; }
        bool IsActive { get; set; }
        bool IsQueryString { get; }
        void InhabiltarDelete(bool op);
        void InhabiltarTodos(bool op);
        
        #endregion

    }
}