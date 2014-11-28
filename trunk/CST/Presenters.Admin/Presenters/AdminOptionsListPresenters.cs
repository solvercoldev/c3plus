using System;
using Application.Core;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;

namespace Presenters.Admin.Presenters
{
    public class AdminOptionsListPresenters: Presenter<IAdminOptionList>
    {
        private readonly ISfTBL_Admin_OptionListManagementServices _optionList;

        public AdminOptionsListPresenters(ISfTBL_Admin_OptionListManagementServices optionList)
        {
            _optionList = optionList;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.FilterEvent += ViewFilterEvent;
            View.PagerEvent += ViewPagerEvent;
        }

        void ViewPagerEvent(object sender, EventArgs e)
        {
            GetAll(sender == null ? 0 : Convert.ToInt32(sender));
        }

        void ViewFilterEvent(object sender, EventArgs e)
        {
            GetAll(0);
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
                var total = _optionList.CountByPaged(View.Search);

                View.TotalRegistrosPaginador = total == 0 ? 1 : total;

                var listado = _optionList.FindPaged(currentPage, View.PageZise, View.Search);

                View.GetOptionsList(listado);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}
