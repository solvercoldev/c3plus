using System;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;
using System.Linq;

namespace Presenters.Admin.Presenters
{
    public class IFrmViewLocalizacionesPresenter : Presenter<IFrmViewLocalizacionesView>
    {
        private readonly ISfLocalizacionesManagementServices _localizaciones;

        public IFrmViewLocalizacionesPresenter(ISfLocalizacionesManagementServices localizaciones)
        {
            _localizaciones = localizaciones;
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

                var listado = _localizaciones.FindPaged(currentPage, View.PageZise);

                View.GetLocalizaciones(listado.OrderBy(o => o.IdLocalizacion).ToList());

            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}