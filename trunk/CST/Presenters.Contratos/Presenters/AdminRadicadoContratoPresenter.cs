using System;
using System.Linq;
using System.Reflection;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Contratos.IViews;
using System.Collections.Generic;
using Application.MainModule.Communication.IServices;
using System.Threading;

namespace Presenters.Contratos.Presenters
{
    public class AdminRadicadoContratoPresenter : Presenter<IAdminRadicadoContratoView>
    {
        readonly ISfContratosManagementServices _contratoService;
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;
        readonly ISfRadicadosManagementServices _radicadoService;
        readonly ISfDocumentosRadicadoManagementServices _documentoRadicadoService;
        readonly IContratoMailService _contratoMailService;
        readonly ISfLogContratosManagementServices _log;

        public AdminRadicadoContratoPresenter(ISfContratosManagementServices contratoService,
                                                ISfTBL_Admin_UsuariosManagementServices usuariosService,
                                                ISfRadicadosManagementServices radicadoService,
                                                ISfDocumentosRadicadoManagementServices documentoRadicadoService,
                                                IContratoMailService contratoMailService,
                                                ISfLogContratosManagementServices log)
        {
            _contratoService = contratoService;
            _usuariosService = usuariosService;
            _radicadoService = radicadoService;
            _documentoRadicadoService = documentoRadicadoService;
            _contratoMailService = contratoMailService;
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
            InitView();
            LoadRadicado();
        }

        void InitView()
        {
            LoadResponsables();
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

        void LoadRadicado()
        {
            if (string.IsNullOrEmpty(View.IdRadicado)) return;

            try
            {
                var radicado = _radicadoService.GetCompleteById(Convert.ToInt64(View.IdRadicado));

                if (radicado != null)
                {
                    View.Numero = string.Format("{0} - ",radicado.Numero);
                    View.Estado = radicado.EstadoRadicado;
                    View.FechaVencimiento = string.Format("{0:dd MMMM yyyy}", radicado.FechaReciboSalida);
                    View.FechaCreacion = string.Format("{0:dd MMMM yyyy}", radicado.CreateOn);
                    View.Asunto = radicado.Asunto;

                    if (radicado.TipoRadicado == "RE")
                    {
                        View.EnviadoPor = radicado.IdFromExterno;
                        View.DirigidoA = radicado.TBL_Admin_Usuarios4.Nombres;
                    }
                    else
                    {
                        View.EnviadoPor = radicado.TBL_Admin_Usuarios3.Nombres;
                        View.DirigidoA = radicado.IdToExterno;
                    }
                    
                    View.Descripcion = radicado.Resumen;
                    View.FechaReprogramacion = radicado.FechaReciboSalida;

                    if (radicado.IdRadicadoEntrada.HasValue)
                    {
                        View.REAsociado = radicado.Radicados2.Numero;                        
                        View.ShowREAsociado(true);
                    }

                    View.MsgLogInfo = string.Format("Creado por {0} en {1:dd/MM/yyyy hh:mm tt}. Modificado por {2} en {3:dd/MM/yyyy hh:mm tt}.",
                                                    radicado.TBL_Admin_Usuarios.Nombres, radicado.CreateOn,
                                                    radicado.TBL_Admin_Usuarios1.Nombres, radicado.ModifiedOn);

                    View.EnableEdit(false);
                    View.EnableActions(radicado.EstadoRadicado != "Respondido" && radicado.EstadoRadicado != "Anulado"
                                        && (View.UserSession.IsInRole("Administrador") || radicado.ResponsableRespuesta == View.UserSession.IdUser));

                    View.EnableMarcarOK(radicado.TipoRadicado == "RS");

                    if (radicado.RespuestaPendiente)
                    {
                        View.ResponsableRespuesta = radicado.TBL_Admin_Usuarios2.Nombres;
                        View.FechaVencimiento = string.Format("{0:dd MMMM yyyy}", radicado.FechaRespuesta);
                        View.FechaRespuesta = string.Format("{0:dd MMMM yyyy}", radicado.FechaRespuesta);
                        View.FechaAlarmaRespuesta = string.Format("{0:dd MMMM yyyy}", radicado.FechaRespuesta.GetValueOrDefault().AddDays(radicado.DiasAlarma.GetValueOrDefault() * (-1)));

                        View.ShowRespuesta(radicado.RespuestaPendiente);

                        View.EnableReasignar(true);
                        View.EnableReprogramar(true);
                        View.EnableMarcarOK(true);
                    }
                    else
                    {
                        View.EnableReasignar(false);
                        View.EnableReprogramar(false);
                        View.EnableResponsableVence(false);
                        View.EnableMarcarOK(false);
                    }

                    LoadContrato();
                    LoadDocumentoRadicado();                    
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadContrato()
        {
            if (string.IsNullOrEmpty(View.IdContrato)) return;

            try
            {
                var model = _contratoService.FindById(Convert.ToInt32(View.IdContrato));

                if (model != null)
                {
                    View.InfoContrato = string.Format("{0} - {1}", model.Nombre, model.NumeroContrato);
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

        public void SendNotifyMail()
        {
            object[] parameters = new object[3];
            parameters[0] = Convert.ToInt64(View.IdRadicado);
            parameters[1] = Convert.ToInt32(View.IdModule);
            parameters[2] = ServerHostPath;

            Thread mailThread = new Thread(_contratoMailService.SendRadicadoMailNotification);
            mailThread.Start(parameters);
        }
      
        public void SaveRadicado()
        {
            if (string.IsNullOrEmpty(View.IdRadicado)) return;

            try
            {               
                LoadRadicado();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void AddNovedadRadicado()
        {
            if (string.IsNullOrEmpty(View.IdRadicado)) return;

            try
            {
                var radicado = _radicadoService.GetById(Convert.ToInt64(View.IdRadicado));

                var model = _contratoService.FindById(Convert.ToInt32(View.IdContrato));

                if (radicado != null)
                {
                    radicado.ModifiedBy = View.UserSession.IdUser;
                    radicado.ModifiedOn = DateTime.Now;

                    switch (View.TipoOperacion)
                    {
                        case "Anular":
                            radicado.EstadoRadicado = "Anulado";
                            break;
                        case "Confirmar":
                            radicado.EstadoRadicado = "Respondido";
                            break;
                        case "Reprogramar":
                            radicado.FechaReciboSalida = View.FechaReprogramacion;
                            radicado.FechaRespuesta = View.FechaReprogramacion;
                            break;
                        case "ReAsignar":
                            radicado.ResponsableRespuesta = View.ResponsableReprogramacion;
                            break;
                    }

                    _radicadoService.Modify(radicado);

                    var log = GetLog();
                    log.Descripcion = string.Format("El usuario [{0}], ha [{1}] el radicado con número [{2}]. Comentarios: [{3}]"
                                                    , View.UserSession.Nombres
                                                    , View.TipoOperacion
                                                    , radicado.Numero
                                                    , View.ObservacionesNovedad);
                    _log.Add(log);

                    model.ModifiedBy = View.UserSession.IdUser;
                    model.ModifiedOn = log.CreateOn.GetValueOrDefault();

                    _contratoService.Modify(model);
                }                

                LoadRadicado();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
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