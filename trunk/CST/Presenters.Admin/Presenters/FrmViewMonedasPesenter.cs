using System;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;
using System.Linq;

namespace Presenters.Admin.Presenters
{
    public class FrmViewMonedasPesenter : Presenter<IFrmMonedasView>
    {
        private readonly ISfMonedasManagementServices _monedas;

        public FrmViewMonedasPesenter(ISfMonedasManagementServices monedas)
        {
            _monedas = monedas;
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
                var total = _monedas.CountByPaged();

                View.TotalRegistrosPaginador = total == 0 ? 1 : total;

                var listado = _monedas.FindPaged(currentPage, View.PageZise);

                View.GetMonedas(listado.OrderBy(o => o.IdMoneda).ToList());

            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}