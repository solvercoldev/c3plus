using System;
using Application.Core;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.Logging;
using Infrastructure.CrossCutting;
using Presenters.Admin.IViews;

namespace Presenters.Admin.Presenters
{
    public class RolePresenter : Presenter<IRoleView>
    {

        private readonly ISfTBL_Admin_RolesManagementServices _iRoleManagementServices;
        private readonly ITraceManager _traceManager;

        public RolePresenter(ISfTBL_Admin_RolesManagementServices iRoleManagementServices, ITraceManager traceManager)
        {
            _iRoleManagementServices = iRoleManagementServices;
            _traceManager = traceManager;
        }


        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.FilterVEvent += ViewFilterVEvent;
        }

        void ViewFilterVEvent(object sender, EventArgs e)
        {
            GetAll(sender==null?0:Convert.ToInt32(sender));
            View.Role = string.Empty;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (!View.IsPostBack)
            {
                GetAll(0);
            }
        }

        private void GetAll(int currentPage)
        {
            try
            {
                var total = _iRoleManagementServices.CountByFilter(View.Role);
                View.TotalRegistrosPaginador = total == 0 ? 1 : total;
                var listado = _iRoleManagementServices.FindRoleByFilter(View.Role, currentPage, View.PageZise);
                View.GetAll(listado);
            }
            catch (Exception e)
            {
                _traceManager.LogInfo(string.Format("AdminEcollectOpenCardPresenter-GetAll, Error: {0}",
                                                   e.InnerException == null ? e.Message : e.InnerException.Message), LogType.Notify);
                InvokeMessageBox(new MessageBoxEventArgs("Error al obtener el listado de roles desde el repositorio de Datos.", TypeError.Error));
            }

        }
    }
}