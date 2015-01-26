using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Contratos.IViews;

namespace Presenters.Contratos.Presenters
{
    public class ManualesANHPresenter : Presenter<IManualesANHView>
    {
        readonly ISfManualAnhManagementServices _manualAnhService;

        public ManualesANHPresenter(ISfManualAnhManagementServices manualAnhService)
        {
            _manualAnhService = manualAnhService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            LoadInit();
        }

        public void LoadInit()
        {
            LoadManuales();
        }

        void LoadManuales()
        {
            try
            {
                var items = _manualAnhService.FindBySpec(true);

                View.LoadManuales(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}