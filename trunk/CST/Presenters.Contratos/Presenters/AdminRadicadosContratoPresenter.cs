using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Contratos.IViews;

namespace Presenters.Contratos.Presenters
{
    public class AdminRadicadosContratoPresenter : Presenter<IAdminRadicadosContratoView>
    {
        readonly ISfContratosManagementServices _contratoService;
        readonly ISfRadicadosManagementServices _radicadoService;        
        readonly ISfLogContratosManagementServices _log;

        public AdminRadicadosContratoPresenter(ISfContratosManagementServices contratoService,
                                                ISfRadicadosManagementServices radicadoService,
                                                ISfLogContratosManagementServices log)
        {
            _contratoService = contratoService;
            _radicadoService = radicadoService;
            _log = log;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            LoadInit();
            InitView();
        }

        public void LoadInit()
        {
            LoadRadicados();
        }

        void InitView()
        {
        }
        
        public void LoadRadicados()
        {
            if (string.IsNullOrEmpty(View.IdContrato)) return;
            try
            {
                var items = _radicadoService.GetByContratoTipoEstadoText(Convert.ToInt32(View.IdContrato), View.TipoRadicado, View.EstadoRadicado, View.SearchText);
                View.LoadRadicados(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }              
    }
}