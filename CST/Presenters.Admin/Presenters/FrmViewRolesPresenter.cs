using System;
using Application.Core;
using Applications.MainModule.Admin.Services;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;
using System.Linq;


namespace Presenters.Admin.Presenters
{
    public class FrmViewRolesPresenter : Presenter<IFrmViewRolesView>
    {
        private readonly SfTBL_Admin_RolesManagementServices _roles;

        public FrmViewRolesPresenter(SfTBL_Admin_RolesManagementServices roles)
        {
            _roles = roles;
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
                var total = _roles.CountByPaged();

                View.TotalRegistrosPaginador = total == 0 ? 1 : total;

                var listado = _roles.FindPaged(currentPage, View.PageZise);

                View.GetRoles(listado.OrderBy(o => o.IdRol).ToList());

            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}