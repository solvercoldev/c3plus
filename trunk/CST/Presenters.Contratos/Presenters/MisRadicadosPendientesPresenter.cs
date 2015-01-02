using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Contratos.IViews;
using Application.MainModule.SqlServices.IServices;

namespace Presenters.Contratos.Presenters
{
    public class MisRadicadosPendientesPresenter : Presenter<IMisRadicadosPendientesView>
    {
        readonly IContratosAdoService _contratoAdoService;

        public MisRadicadosPendientesPresenter(IContratosAdoService contratoAdoService)
        {
            _contratoAdoService = contratoAdoService;
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
            try
            {
                var dt = _contratoAdoService.GetRadicadosPendientesView(View.UserSession.IdUser);

                View.LoadRadicados(dt);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

    }
}