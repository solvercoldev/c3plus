using System;
using Application.Core;
using Applications.MainModule.Admin.Services;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;
using System.Linq;

namespace Presenters.Admin.Presenters
{
    public class FrmViewUsuariosPresenter : Presenter<IFrmViewUsuariosView>
    {
        private readonly SfTBL_Admin_UsuariosManagementServices _usuario;

        public FrmViewUsuariosPresenter(SfTBL_Admin_UsuariosManagementServices usuario)
        {
            _usuario = usuario;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.FilterEvent += ViewFilterEvent;
        }

        void ViewFilterEvent(object sender, EventArgs e)
        {
            GetAll(sender == null ? 0 : Convert.ToInt32(sender));
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            GetAll(0);
        }

        private void GetAll(int currentPage)
        {
            try
            {

                var listado = _usuario.FindPaged(currentPage, View.PageZise);

                View.GetUsuarios(listado.OrderBy(o => o.Nombres).ToList());

            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}