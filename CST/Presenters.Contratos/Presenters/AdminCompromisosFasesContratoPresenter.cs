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
    public class AdminCompromisosFasesContratoPresenter : Presenter<IAdminCompromisosFasesContratoView>
    {
        readonly ISfContratosManagementServices _contratoService;
        readonly ISfCompromisosManagementServices _compromisosService;
        readonly ISfFasesManagementServices _fasesService;
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;

        public AdminCompromisosFasesContratoPresenter(ISfContratosManagementServices contratoService,
                                                ISfCompromisosManagementServices compromisosService,
                                                ISfFasesManagementServices fasesService,
                                                ISfTBL_Admin_UsuariosManagementServices usuariosService)
        {
            _contratoService = contratoService;
            _compromisosService = compromisosService;
            _fasesService = fasesService;
            _usuariosService = usuariosService;
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
            LoadResponsables();
            LoadCompromisos();

            View.CanAddCompromisos = View.UserSession.IsInRole("Administrador");
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

        void LoadResponsables()
        {
            try
            {
                var items = _usuariosService.FindBySpec(true);
                View.LoadResponsables(items);
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
                var items = _compromisosService.GetByContrato(Convert.ToInt32(View.IdContrato));
                View.LoadCompromisos(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
      
        public void SaveCompromiso()
        {
            try
            {
                var model = GetModel();
                _compromisosService.Add(model);
                LoadCompromisos();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        Compromisos GetModel()
        {
            var model = new Compromisos();

            model.IdFase = View.IdFase;
            model.TipoCompromiso = "";
            model.Nombre = View.Nombre;
            model.Descripcion = View.Descripcion;
            model.Importancia = "";
            model.FechaCumplimiento = View.FechaCumplimiento;
            model.Estado = "";

            return model;
        }
    }
}