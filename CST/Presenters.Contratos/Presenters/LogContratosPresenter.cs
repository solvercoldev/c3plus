using System;
using System.Linq;
using System.Reflection;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Contratos.IViews;

namespace Presenters.Contratos.Presenters
{
    public class LogContratosPresenter : Presenter<ILogContratosView>
    {
        private readonly ISfLogContratosManagementServices _log;

        public LogContratosPresenter(ISfLogContratosManagementServices log)
        {
            _log = log;
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

        private void GetAll(int currentePage)
        {
            if (string.IsNullOrEmpty(View.IdContrato)) return;
            try
            {
                var lista = _log.GetByIdContrato(Convert.ToInt32(View.IdContrato)).OrderByDescending(x => x.CreateOn).ToList();
                View.LogsList(lista);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

    }
}