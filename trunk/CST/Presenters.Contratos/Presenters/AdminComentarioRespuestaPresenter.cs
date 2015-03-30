using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Contratos.IViews;

namespace Presenters.Contratos.Presenters
{
    public class AdminComentarioRespuestaPresenter : Presenter<IAdminComentarioRespuestaView>
    {
        readonly ISfContratosManagementServices _contratoService;
        readonly ISfComentariosRespuestaManagementServices _comentariosService;
        readonly ISfAnexosComentarioRespuestaManagementServices _anexosService;
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;

        public AdminComentarioRespuestaPresenter(ISfContratosManagementServices contratoService,
                                                 ISfComentariosRespuestaManagementServices comentariosService,
                                                 ISfAnexosComentarioRespuestaManagementServices anexosService,
                                                 ISfTBL_Admin_UsuariosManagementServices usuariosService)
        {
            _contratoService = contratoService;
            _comentariosService = comentariosService;
            _anexosService = anexosService;
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
        }

        public void LoadInit()
        {
            LoadComentarioRespuestaReclamo();
            LoadContrato();
            LoadUsuarioDestino();            
        }

        void LoadContrato()
        {
            if (string.IsNullOrEmpty(View.IdContrato)) return;

            try
            {
                var contrato = _contratoService.GetContratoWithNavsById(Convert.ToInt32(View.IdContrato));
                if (contrato != null)
                {
                    View.NombreContrato = contrato.Nombre;
                    View.NumeroContrato = contrato.NumeroContrato;
                    View.Empresa = contrato.Empresas.RazonSocial;
                    View.TipoContrato = contrato.TiposContrato.Descripcion;
                    View.Bloque = contrato.Bloques.Descripcion;
                    View.FechaFirma = string.Format("{0:MMMM} {0:dd} de {0:yyyy}", contrato.FechaFirma);
                    View.FechaFirma = string.Format("{0:MMMM} {0:dd} de {0:yyyy}", contrato.FechaFirma);
                    View.FechaEfectiva = string.Format("{0:MMMM} {0:dd} de {0:yyyy}", contrato.FechaInicio);
                    View.Periodo = string.Format("{0}", UppercaseFirst(string.Format("{0:MMMM} {0:dd} de {0:yyyy}", contrato.FechaTerminacion)));                    
                }                
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadUsuarioDestino()
        {
            try
            {
                var items = _usuariosService.FindBySpec(true);
                View.LoadDestinatarios(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadComentarioRespuestaReclamo()
        {
            if (string.IsNullOrEmpty(View.IdComentario)) return;

            try
            {
                var model = _comentariosService.GetById(Convert.ToDecimal(View.IdComentario));

                if (model != null)
                {
                    View.IdContrato = model.IdContrato.ToString();
                    View.Asunto = model.Asunto;
                    View.Mensaje = model.Comentario;
                    View.Destinatario = model.TBL_Admin_Usuarios2.Nombres;
                    View.FechaComentario = model.CreateOn;
                    View.IdUsuarioDestino = model.CreateBy.ToString();
                    var usuariosCopia = new List<DTO_ValueKey>();
                    if (model.TBL_Admin_Usuarios3.Any())
                    {
                        foreach (var itm in model.TBL_Admin_Usuarios3)
                        {
                            var usuarioCopia = new DTO_ValueKey() { Id = itm.IdUser.ToString(), Value = itm.Nombres };
                            usuariosCopia.Add(usuarioCopia);
                        }
                    }
                    View.LoadUsuariosCopia(usuariosCopia);

                    View.EnableEdit(false);
                    LoadComentarioRelacionados();
                    LoadArhchivosAdjuntos();
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadComentarioRelacionados()
        {
            if (string.IsNullOrEmpty(View.IdComentario)) return;

            try
            {
                var items = _comentariosService.GetByIdComentarioRelacionado(Convert.ToDecimal(View.IdComentario));

                View.LoadComentariosRelacionados(items);

            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void AddComentarioRelacionado()
        {
            if (string.IsNullOrEmpty(View.IdComentario)) return;

            try
            {
                var model = GetModel();

                _comentariosService.Add(model);

                LoadComentarioRespuestaReclamo();

                try
                {
                    //_senMailServices.EnviarCorreoelectronicoAutorReclamo(model.IdComentario, View.UserSession);
                }
                catch (Exception ex)
                {
                    CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                }

            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void AddArchivoAdjunto()
        {
            try
            {
                var archivo = new AnexosComentarioRespuesta();
                archivo.IdComentario = Convert.ToDecimal(View.IdComentario);
                archivo.NombreArchivo = View.NombreArchivoAdjunto;
                archivo.Archivo = View.ArchivoAdjunto;
                archivo.CreateBy = View.UserSession.IdUser;
                archivo.CreateOn = DateTime.Now;
                archivo.ModifiedBy = View.UserSession.IdUser;
                archivo.ModifiedOn = DateTime.Now;

                _anexosService.Add(archivo);

                LoadArhchivosAdjuntos();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void RemoveArchivoAdjunto(decimal idArchivo)
        {
            try
            {
                var model = _anexosService.GetById(idArchivo);

                if (model != null)
                {
                    _anexosService.Remove(model);

                    LoadArhchivosAdjuntos();
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void DownloadArchivoAdjunto(decimal idArchivo)
        {
            try
            {
                var model = _anexosService.GetById(idArchivo);

                if (model != null)
                {
                    var archivo = new DTO_ValueKey();
                    archivo.ComplexValue = model.Archivo;
                    archivo.Id = string.Format("{0}", model.IdAnexoComentario);
                    archivo.Value = model.NombreArchivo;

                    View.DescargarArchivo(archivo);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadArhchivosAdjuntos()
        {
            if (string.IsNullOrEmpty(View.IdComentario)) return;

            try
            {
                var anexos = _anexosService.GetByComentarioId(Convert.ToDecimal(View.IdComentario));
                var archivosAdjuntos = new List<DTO_ValueKey>();
                if (anexos.Any())
                {
                    foreach (var anexo in anexos)
                    {
                        var archivo = new DTO_ValueKey();
                        archivo.Id = string.Format("{0}", anexo.IdAnexoComentario);
                        archivo.Value = anexo.NombreArchivo;
                        archivo.ComplexValue = anexo.Archivo;
                        archivo.CreateBy = anexo.CreateBy;

                        archivosAdjuntos.Add(archivo);
                    }
                }
                View.LoadArchivosAdjuntos(archivosAdjuntos);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        ComentariosRespuesta GetModel()
        {
            var model = new ComentariosRespuesta();

            model.IdContrato = Convert.ToInt32(View.IdContrato);
            model.Asunto = View.Asunto;
            model.Comentario = View.NuevoComentario;
            model.IdComentarioRelacionado = Convert.ToDecimal(View.IdComentario);
            model.IdUsuarioDestino = Convert.ToInt32(View.IdUsuarioDestino);
            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;
            model.ModifiedBy = View.UserSession.IdUser;
            model.ModifiedOn = DateTime.Now;

            return model;
        }
    }
}