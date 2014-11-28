using System;
using System.Linq;
using Application.Core;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;

namespace Presenters.Admin.Presenters
{
    public class ModulesPresenter : Presenter<IModulesView>
    {
        private readonly ISfTBL_Admin_ModuleTypeManagementServices _modulesServices;

        public ModulesPresenter(ISfTBL_Admin_ModuleTypeManagementServices modulesServices)
        {
            _modulesServices = modulesServices;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.FilterEvent += ViewFilterEvent;
            View.UpdateEvent += ViewUpdateEvent;
        }

        void ViewUpdateEvent(object sender, EventArgs e)
        {
            if(sender==null)return;
            var mt = sender as TBL_Admin_ModuleType;
            UpdateModuleType(mt);
        }

        void ViewFilterEvent(object sender, EventArgs e)
        {
            GetAll(sender==null ? 0 : Convert.ToInt32(sender));
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if(View.IsPostBack)return;
            GetAll(0);
        }

        private void GetAll(int currentPage)
        {
            try
            {
                var total = _modulesServices.FindBySpec(true).Count;
                View.TotalRegistrosPaginador = total == 0 ? 1:total;
                var list = _modulesServices.FindPaged(currentPage, View.PageZise);
                View.GetModules(list.ToList());
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        } 

        public TBL_Admin_ModuleType GetById(int id)
        {
            try
            {
                return _modulesServices.FindById(id);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
            return null;
        }

        private void UpdateModuleType(TBL_Admin_ModuleType mt)
        {
            try
            {
                if(mt == null)return;
                _modulesServices.Modify(mt);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}