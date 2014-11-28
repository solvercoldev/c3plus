using System;
using System.Collections.Generic;
using Domain.MainModules.Entities;

namespace Presenters.Admin.IViews
{
    public interface IRoleView : Application.Core.IView
    {
        #region Events
        /// <summary>
        /// Evento cuando el usuario Filtra los Registros
        /// </summary>
        event EventHandler FilterVEvent;

        #endregion

        #region Members
        void GetAll(IEnumerable<TBL_Admin_Roles> items);
        string Role { get; set; }
        int TotalRegistrosPaginador { set; }
        int PageZise { get; }
        #endregion
    }
}