using System;
using System.Linq;
using Application.Core;
using Presenters.Admin.IViews;

namespace Presenters.Admin.Presenters
{
    public class LogoutPresenter : Presenter<ILogoutView>
    {
        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            LoadData();
        }


        private void LoadData()
        {
            View.User = View.UserSession.Nombres;
            View.Role = View.UserSession.TBL_Admin_Roles.Select(x => x.NombreRol).FirstOrDefault();
        }

    }
}