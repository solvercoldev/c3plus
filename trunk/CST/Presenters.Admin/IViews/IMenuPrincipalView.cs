using System.Collections.Generic;
using Domain.MainModules.Entities;

namespace Presenters.Admin.IViews
{
    public interface IMenuPrincipalView : Application.Core.IView
    {
       
        #region Members

        void OpcionesMenu(IEnumerable<TBL_Admin_OpcionesMenu> items);

        #endregion
    }

    public interface IMenuSecundarioView : Application.Core.IView
    {

        #region Members

        void OpcionesMenu(IEnumerable<TBL_Admin_OpcionesMenu> items);
        string IdMenuParent { get; }
        #endregion
    }
}