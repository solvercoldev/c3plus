using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Contratos.IViews;
using Application.MainModule.SqlServices.IServices;
using Application.MainModule.Communication.IServices;
using System.Threading;

namespace Presenters.Contratos.Presenters
{
    public class NewRadicadoContratoPresenter : Presenter<INewRadicadoContratoView>
    {
        readonly ISfContratosManagementServices _contratoService;
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;
        readonly ISfRadicadosManagementServices _radicadoService;
        readonly ISfDocumentosRadicadoManagementServices _documentoRadicadoService;
        readonly IContratoMailService _contratoMailService;
        readonly IContratosAdoService _adoService;
        readonly ISfLogContratosManagementServices _log;

        public NewRadicadoContratoPresenter(ISfContratosManagementServices contratoService,
                                                ISfTBL_Admin_UsuariosManagementServices usuariosService,
                                                ISfRadicadosManagementServices radicadoService,
                                                ISfDocumentosRadicadoManagementServices documentoRadicadoService,
                                                IContratoMailService contratoMailService,
                                                IContratosAdoService adoService,
                                                ISfLogContratosManagementServices log)
        {
            _contratoService = contratoService;
            _usuariosService = usuariosService;
            _radicadoService = radicadoService;
            _documentoRadicadoService = documentoRadicadoService;
            _contratoMailService = contratoMailService;
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
            InitView();
            LoadInit();

            if (!string.IsNullOrEmpty(View.IdRadicado))
                LoadRadicado();
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

        void LoadRadicado()
        {
            if (string.IsNullOrEmpty(View.IdRadicado)) return;

            try
            {
                var model = _radicadoService.GetCompleteById(Convert.ToInt64(View.IdRadicado));

                if (model != null)
                {
                    View.TipoRadicado = model.TipoRadicado;
                    View.Numero = model.Numero;
                    View.FechaRadicado = model.FechaReciboSalida;
                    View.Asunto = model.Asunto;

                    if (model.TipoRadicado == "RE")
                    {
                        View.EnviadoPor = model.IdFromExterno;
                        View.DirigidoA = model.IdTo.GetValueOrDefault();
                    }
                    else
                    {
                        View.IdEnviadoPor = model.IdFrom.GetValueOrDefault();
                        View.DirigidoAExterno = model.IdToExterno;
                    }
                    
                    View.Resumen = model.Resumen;
                    View.RespuestaPendiente = model.RespuestaPendiente;

                    View.ShowRespondeRadicadoSalida(model.TipoRadicado == "RS");

                    if (model.RespuestaPendiente)
                    {
                        View.FechaRespuesta = model.FechaRespuesta.GetValueOrDefault();
                        View.ResponsableRespuesta = model.ResponsableRespuesta.GetValueOrDefault();
                        View.DiasAlarma = model.DiasAlarma.GetValueOrDefault();

                        View.ShowRespuestaRadicado(true);
                    }

                    if (model.IdRadicadoEntrada.HasValue)
                    {
                        View.ShowRespondeRadicadoSalida(true);
                        View.ShowRadicadoSalida(true);
                        View.RespondeRE = true;
                        View.IdRadicadoEntradaAsociado = model.IdRadicadoEntrada.GetValueOrDefault();
                    }                        

                    LoadDocumentoRadicado();
                }

            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadDocumentoRadicado()
        {
            if (string.IsNullOrEmpty(View.IdRadicado)) return;

            try
            {
                var doc = _documentoRadicadoService.GetByIdRadicado(Convert.ToInt64(View.IdRadicado));
                if (doc != null)
                {
                    var dto = new DTO_ValueKey();
                    dto.Id = string.Format("{0}", doc.IdDocumentoRadicado);
                    dto.Value = doc.NombreArchivo;
                    dto.ComplexValue = doc.Archivo;

                    View.ArchivoAdjunto = dto;
                }
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

                if (model.IdRadicadoEntrada.HasValue)
                {
                    var radicadoEntrada = _radicadoService.GetById(model.IdRadicadoEntrada.GetValueOrDefault());

                    radicadoEntrada.EstadoRadicado = "Respondido";
                    radicadoEntrada.ModifiedBy = View.UserSession.IdUser;
                    radicadoEntrada.ModifiedOn = DateTime.Now;

                    _radicadoService.Modify(radicadoEntrada);
                }

                if (model.RespuestaPendiente)
                    SendNotifyMail(model);

                View.GoToRadicadoView(model.IdRadicado);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void SendNotifyMail(Radicados radicado)
        {
            object[] parameters = new object[3];
            parameters[0] = radicado.IdRadicado;
            parameters[1] = Convert.ToInt32(View.IdModule);
            parameters[2] = ServerHostPath;

            Thread mailThread = new Thread(_contratoMailService.SendRadicadoMailNotification);
            mailThread.Start(parameters);
        }

        public void UpdateRadicado()
        {
            try
            {
                var model = _radicadoService.GetById(Convert.ToInt64(View.IdRadicado));

                model.IdContrato = Convert.ToInt32(View.IdContrato);
                model.TipoRadicado = View.TipoRadicado;
                model.Numero = View.Numero;
                model.FechaReciboSalida = View.FechaRadicado;
                model.Asunto = View.Asunto;

                if (model.TipoRadicado == "RE")
                {
                    model.IdFromExterno = View.EnviadoPor;
                    model.IdTo = View.DirigidoA;
                }
                else
                {
                    model.IdFrom = View.IdEnviadoPor;
                    model.IdToExterno = View.DirigidoAExterno;
                }

                model.Resumen = View.Resumen;
                model.RespuestaPendiente = View.RespuestaPendiente;                

                if (View.RespuestaPendiente)
                {
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
                else
                    model.IdRadicadoEntrada = null;

                model.ModifiedBy = View.UserSession.IdUser;
                model.ModifiedOn = DateTime.Now;

                _radicadoService.Modify(model);
                var contrato = _contratoService.FindById(Convert.ToInt32(View.IdContrato));

                if (!string.IsNullOrEmpty(View.NombreAnexo))
                    AddDocumentoRadicado(model.IdRadicado);

                var log = GetLog();
                log.Descripcion = string.Format("El usuario [{0}], ha modificado radicado [{1}].", View.UserSession.Nombres, model.Numero);
                _log.Add(log);

                contrato.ModifiedBy = View.UserSession.IdUser;
                contrato.ModifiedOn = DateTime.Now;

                _contratoService.Modify(contrato);


                if (model.IdRadicadoEntrada.HasValue)
                {
                    var radicadoEntrada = _radicadoService.GetById(model.IdRadicadoEntrada.GetValueOrDefault());

                    radicadoEntrada.EstadoRadicado = "Respondido";
                    radicadoEntrada.ModifiedBy = View.UserSession.IdUser;
                    radicadoEntrada.ModifiedOn = DateTime.Now;

                    _radicadoService.Modify(radicadoEntrada);
                }

                if (model.RespuestaPendiente)
                    SendNotifyMail(model);

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
                _adoService.DeleteDocumentoRadicado(idRadicado);
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
            model.Resumen = View.Resumen;
            model.RespuestaPendiente = View.RespuestaPendiente;
            switch (View.TipoRadicado)
            {
                case "RE":
                    model.EstadoRadicado = "Radicado";
                    model.IdFromExterno = View.EnviadoPor;
                    model.IdTo = View.DirigidoA;
                    break;
                case "RS":
                    model.EstadoRadicado = "Enviado";
                    model.IdFrom = View.IdEnviadoPor;
                    model.IdToExterno = View.DirigidoAExterno;
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