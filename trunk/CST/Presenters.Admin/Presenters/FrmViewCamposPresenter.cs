using System;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;
using System.Linq;

namespace Presenters.Admin.Presenters
{
    public class FrmViewCamposPresenter : Presenter<IFrmViewCamposView>
    {
        private readonly ISfCamposManagementServices _campos;

        public FrmViewCamposPresenter(ISfCamposManagementServices campos)
        {
            _campos = campos;
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
                var total = _campos.CountByPaged();

                View.TotalRegistrosPaginador = total == 0 ? 1 : total;

                var listado = _campos.FindPaged(currentPage, View.PageZise);

                View.GetCampos(listado.OrderBy(o=>o.IdCampo).ToList());

            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}