using System;
using Application.Core;
using Applications.MainModule.Admin.IServices;
using Presenters.Admin.IViews;

namespace Presenters.Admin.Presenters
{
    public class MenuPrincipalPresenter : Presenter<IMenuPrincipalView>
    {

        private readonly ISfTBL_Admin_OpcionesMenuManagementServices _iMenuManagementServices;

        public MenuPrincipalPresenter(ISfTBL_Admin_OpcionesMenuManagementServices iMenuManagementServices)
        {
            _iMenuManagementServices = iMenuManagementServices;
        }
        
        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if(View.IsPostBack)return;
            GetOpcionesMenuPrincipal();
        }

        private void GetOpcionesMenuPrincipal()
        {
            var opciones = _iMenuManagementServices.FindListByIdModule(View.IdModule);
            View.OpcionesMenu(opciones);
        }
    }
}