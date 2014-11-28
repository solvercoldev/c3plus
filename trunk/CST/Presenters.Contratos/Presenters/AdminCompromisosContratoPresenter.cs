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
    public class AdminCompromisosContratoPresenter : Presenter<IAdminCompromisosContratoView>
    {
        readonly ISfContratosManagementServices _contratoService;
        readonly ISfCompromisosManagementServices _compromisosService;
        readonly ISfFasesManagementServices _fasesService;
        readonly ISfLogContratosManagementServices _log;

        public AdminCompromisosContratoPresenter(ISfContratosManagementServices contratoService,
                                                ISfCompromisosManagementServices compromisosService,
                                                ISfFasesManagementServices fasesService,
                                                ISfLogContratosManagementServices log)
        {
            _contratoService = contratoService;
            _compromisosService = compromisosService;
            _fasesService = fasesService;
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
            LoadFases();
            LoadCompromisos();
        }

        void InitView()
        {
        }

        void LoadFases()
        {
            if (string.IsNullOrEmpty(View.IdContrato)) return;

            try
            {
                var items = _fasesService.GetFasesByContrato(Convert.ToInt32(View.IdContrato));
                View.LoadFases(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
        
        public void LoadCompromisos()
        {
            if (string.IsNullOrEmpty(View.IdContrato)) return;
            try
            {
                var items = _compromisosService.GetByContratoFase(Convert.ToInt32(View.IdContrato), View.IdFase);
                View.LoadCompromisos(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }              
    }
}