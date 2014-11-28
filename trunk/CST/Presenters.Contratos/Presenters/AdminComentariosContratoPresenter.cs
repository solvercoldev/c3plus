using System;
using System.Linq;
using System.Reflection;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Contratos.IViews;
using Application.MainModule.SqlServices.IServices;

namespace Presenters.Contratos.Presenters
{
    public class AdminComentariosContratoPresenter : Presenter<IAdminComentariosContratoView>
    {
        readonly ISfContratosManagementServices _contratoService;
        readonly ISfComentariosRespuestaManagementServices _comentariosService;
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;
        readonly ISfAnexosComentarioRespuestaManagementServices _anexosService;
        readonly IContratosAdoService _contratoAdoService;

        public AdminComentariosContratoPresenter(ISfContratosManagementServices contratoService,
                                                ISfComentariosRespuestaManagementServices comentariosService,
                                                ISfTBL_Admin_UsuariosManagementServices usuariosService,
                                                ISfAnexosComentarioRespuestaManagementServices anexosService,
                                                IContratosAdoService contratoAdoService)
        {
            _contratoService = contratoService;
            _comentariosService = comentariosService;
            _usuariosService = usuariosService;
            _anexosService = anexosService;
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
            InitView();
        }

        public void LoadInit()
        {
            LoadDestinatarios();
            LoadComentarios();
            LoadUsuarioCopia();
        }

        void InitView()
        {

        }

        void LoadDestinatarios()
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

        public void LoadComentarios()
        {
            if (string.IsNullOrEmpty(View.IdContrato)) return;
            try
            {
                var items = _comentariosService.GetByContrato(Convert.ToInt32(View.IdContrato));
                if (items != null && items.Any())
                {
                    var totalItems = items;
                    items = items.Where(x => x.IdComentarioRelacionado == null).ToList();
                    foreach (var itm in items)
                    {
                        var children = totalItems.Where(x => x.IdComentarioRelacionado == itm.IdComentario);
                        if (children != null && children.Any())
                            itm.ComentariosAsociados = children.ToList();
                    }
                }
                View.LoadComentarios(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadUsuarioCopia()
        {
            try
            {
                var items = _usuariosService.FindBySpec(true);
                View.LoadUsuarioCopia(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
      
        public void SaveComentario()
        {
            try
            {
                var model = GetModel();
                _comentariosService.Add(model);

                if (View.UsuariosCopia.Any())
                {
                    foreach (var item in View.UsuariosCopia)
                    {
                      _contratoAdoService.InsertUsuarioCopiaComentario(item.Id, string.Format("{0}", model.IdComentario));
                    }
                }

                if (View.ArchivosAdjuntos.Any())
                {
                    foreach (var archivo in View.ArchivosAdjuntos)
                    {
                        var anexo = new AnexosComentarioRespuesta();
                        anexo.IdComentario = model.IdComentario;
                        anexo.NombreArchivo = archivo.Value;
                        anexo.Archivo = (byte[])archivo.ComplexValue;
                        anexo.IsActive = true;
                        anexo.CreateBy = View.UserSession.IdUser;
                        anexo.CreateOn = DateTime.Now;
                        anexo.ModifiedBy = View.UserSession.IdUser;
                        anexo.ModifiedOn = DateTime.Now;

                        _anexosService.Add(anexo);
                    }
                }

                LoadComentarios();
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
            model.Comentario = View.Comentario;
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