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
    public class NewRadicadoContratoPresenter : Presenter<INewRadicadoContratoView>
    {
        readonly ISfContratosManagementServices _contratoService;
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;
        readonly ISfRadicadosManagementServices _radicadoService;
        readonly ISfDocumentosRadicadoManagementServices _documentoRadicadoService;
        readonly ISfLogContratosManagementServices _log;

        public NewRadicadoContratoPresenter(ISfContratosManagementServices contratoService,
                                                ISfTBL_Admin_UsuariosManagementServices usuariosService,
                                                ISfRadicadosManagementServices radicadoService,
                                                ISfDocumentosRadicadoManagementServices documentoRadicadoService,
                                                ISfLogContratosManagementServices log)
        {
            _contratoService = contratoService;
            _usuariosService = usuariosService;
            _radicadoService = radicadoService;
            _documentoRadicadoService = documentoRadicadoService;
            _log = log;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            InitView();
            LoadInit();            
        }

        public void LoadInit()
        {
            LoadResponsables();
            LoadRadicadosPendientes();
        }

        void InitView()
        {
            View.FechaRadicado = DateTime.Now;
            View.FechaRespuesta = DateTime.Now;
            View.DiasAlarma = 1;
        }             
 
        void LoadResponsables()
        {
            try
            {
                var items = _usuariosService.FindBySpec(true);
                View.LoadUsuarios(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadRadicadosPendientes()
        {
            try
            {
                var items = _radicadoService.GetRadicadosPendienteByContrato(Convert.ToInt32(View.IdContrato));
                View.LoadRadicadosPendientes(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
      
        public void SaveRadicado()
        {
            try
            {
                var model = GetModel();
                _radicadoService.Add(model);
                var contrato = _contratoService.FindById(Convert.ToInt32(View.IdContrato));

                AddDocumentoRadicado(model.IdRadicado);
                
                var log = GetLog();
                log.Descripcion = string.Format("El usuario [{0}], ha ingresado un nuevo radicado.", View.UserSession.Nombres);
                _log.Add(log);

                contrato.ModifiedBy = View.UserSession.IdUser;
                contrato.ModifiedOn = DateTime.Now;

                _contratoService.Modify(contrato);

                View.GoToRadicadoView(model.IdRadicado);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void AddDocumentoRadicado(long idRadicado)
        {
            try
            {
                var model = GetDocumentoModel(idRadicado);
                _documentoRadicadoService.Add(model);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }    

        Radicados GetModel()
        {
            var model = new Radicados();

            model.IdContrato = Convert.ToInt32(View.IdContrato);
            model.TipoRadicado = View.TipoRadicado;
            model.Numero = View.Numero;
            model.FechaReciboSalida = View.FechaRadicado;
            model.Asunto = View.Asunto;
            model.IdFromExterno = View.EnviadoPor;
            model.IdTo = View.DirigidoA;
            model.Resumen = View.Resumen;
            model.RespuestaPendiente = View.RespuestaPendiente;
            switch (View.TipoRadicado)
            {
                case "RE":
                    model.EstadoRadicado = "Radicado";
                    break;
                case "RS":
                    model.EstadoRadicado = "Enviado";
                    break;
            }

            if (View.RespuestaPendiente)
            {
                model.EstadoRadicado = "Pendiente Respuesta";
                model.FechaRespuesta = View.FechaRespuesta;
                model.ResponsableRespuesta = View.ResponsableRespuesta;
                model.DiasAlarma = View.DiasAlarma;
            }
            else
            {
                model.ResponsableRespuesta = View.UserSession.IdUser;
            }

            if (View.RespondeRE)
                model.IdRadicadoEntrada = View.IdRadicadoEntradaAsociado;

            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;
            model.ModifiedBy = View.UserSession.IdUser;
            model.ModifiedOn = DateTime.Now;

            return model;
        }

        DocumentosRadicado GetDocumentoModel(long idRadicado)
        {
            var model = new DocumentosRadicado();

            model.IdDocumentoRadicado = Guid.NewGuid();
            model.IdRadicado = idRadicado;
            model.NombreArchivo = View.NombreAnexo;
            model.Archivo = View.ArchivoAnexo;
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
            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;

            return model;
        }
    }
}