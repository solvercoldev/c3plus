using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Contratos.IViews;
using Domain.MainModules.Entities;
using Application.MainModule.SqlServices.IServices;

namespace Presenters.Contratos.Presenters
{
    public class AdminNovedadesContratoPresenter : Presenter<IAdminNovedadesContratoView>
    {
        readonly ISfContratosManagementServices _contratoService;
        readonly ISfNovedadesContratoManagementServices _novedadesContratoService;
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;
        readonly ISfEstadosAccionManagementServices _estadosAccionService;
        readonly IContratosAdoService _adoService;
        readonly ISfLogContratosManagementServices _log;

        public AdminNovedadesContratoPresenter(ISfContratosManagementServices contratoService,
                                               ISfNovedadesContratoManagementServices novedadesContratoService,
                                               ISfTBL_Admin_UsuariosManagementServices usuariosService,
                                               ISfEstadosAccionManagementServices estadosAccionService,
                                               IContratosAdoService adoService,
                                               ISfLogContratosManagementServices log)
        {
            _contratoService = contratoService;
            _novedadesContratoService = novedadesContratoService;
            _usuariosService = usuariosService;
            _estadosAccionService = estadosAccionService;
            _adoService = adoService;
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
            LoadNovedades();
            LoadContrato();
        }

        void InitView()
        {
            View.FechaNovedad = DateTime.Now;
            View.FechaFinNovedad = DateTime.Now.AddDays(60);
        }

        void LoadContrato()
        {
            if (string.IsNullOrEmpty(View.IdContrato)) return;

            try
            {
                var contrato = _contratoService.FindById(Convert.ToInt32(View.IdContrato));
                if (contrato != null)
                {
                    var estadoAccion = _estadosAccionService.GetByEstado(contrato.Estado);

                    if (estadoAccion != null)
                    {
                        View.CanRenunciar = estadoAccion.Renunciar;
                        View.CanTerminar = estadoAccion.Terminar;
                        View.CanSuspender = estadoAccion.Suspender;
                        View.CanRestituir = estadoAccion.Restituir;
                        View.CanAnular = contrato.Estado != "Anulado" && contrato.Estado != "Terminado" && contrato.Estado != "Vencido";
                    }

                    View.FechaFirma = contrato.FechaFirma;
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadNovedades()
        {
            if (string.IsNullOrEmpty(View.IdContrato)) return;
            try
            {
                var items = _novedadesContratoService.GetNovedadesByContrato(Convert.ToInt32(View.IdContrato));
                View.LoadNovedades(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
      
        public void SaveNovedad()
        {
            try
            {
                var model = GetModel();
                _novedadesContratoService.Add(model);
                ProcessNovedad();
                var contrato = _contratoService.FindById(Convert.ToInt32(View.IdContrato));
                if (View.TipoOperacion != "Modificación Fecha Efectiva")
                    contrato.Estado = GetEstadoByTipoOperacionContrato();
                contrato.ModifiedBy = View.UserSession.IdUser;
                contrato.ModifiedOn = DateTime.Now;
                _contratoService.Modify(contrato);
                var log = GetLog();
                _log.Add(log);

                LoadInit();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void ProcessNovedad()
        {
            switch (View.TipoOperacion)
            {
                case "Suspensión":
                    _adoService.SuspenderContrato(Convert.ToInt32(View.IdContrato), View.FechaNovedad, View.FechaFinNovedad);
                    break;
                case "Reiniciar":
                    _adoService.RestitucionManualContrato(Convert.ToInt32(View.IdContrato), View.FechaNovedad);
                    break;
                case "Renuncia":
                    _adoService.RenunciarContrato(Convert.ToInt32(View.IdContrato));
                    break;
                case "Terminación":
                    _adoService.RenunciarContrato(Convert.ToInt32(View.IdContrato));
                    break;
                case "Anulación":
                    _adoService.RenunciarContrato(Convert.ToInt32(View.IdContrato));
                    break;
                case "Modificación Fecha Efectiva":
                    _adoService.ModificarFechaEfectivaContrato(Convert.ToInt32(View.IdContrato), View.FechaNovedad);
                    break;
            }
        }

        string GetEstadoByTipoOperacionContrato()
        {
            string estado = "Vigente";

            switch (View.TipoOperacion)
            {
                case "Suspensión":
                    estado = "Suspendido";
                    break;
                case "Reiniciar":
                    estado = "Vigente";
                    break;
                case "Renuncia":
                    estado = "Renunciado";
                    break;
                case "Terminación":
                    estado = "Terminado";
                    break;
                case "Anulación":
                    estado = "Anulado";
                    break;
            }

            return estado;
        }

        NovedadesContrato GetModel()
        {
            var model = new NovedadesContrato();

            model.IdContrato = Convert.ToInt32(View.IdContrato);
            model.TipoNovedad = View.TipoOperacion;
            model.Descripcion = View.Descripcion;
            model.FechaInicio = View.FechaNovedad;
            model.FechaFin = View.FechaFinNovedad;
            model.DiasDesplazamiento = 0;
            model.IdResponsable = View.UserSession.IdUser;
            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;
            model.ModifiedBy = View.UserSession.IdUser;
            model.ModifiedOn = DateTime.Now;

            return model;
        }

        LogContratos GetLog()
        {
            var model = new LogContratos();

            model.IdLog = Guid.NewGuid();
            model.IdContrato = Convert.ToInt32(View.IdContrato);
            model.Descripcion = string.Format("El usuario [{0}] ha ingresado una novedad al contrato de tipo [{1}]. Comentarios: [{2}]", View.UserSession.Nombres, View.TipoOperacion, View.Descripcion);
            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;

            return model;
        }
    }
}