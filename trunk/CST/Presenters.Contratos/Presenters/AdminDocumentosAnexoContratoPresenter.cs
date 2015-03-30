using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.Contratos.IServices;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Contratos.IViews;
using System.Collections.Generic;

namespace Presenters.Contratos.Presenters
{
    public class AdminDocumentosAnexoContratoPresenter : Presenter<IAdminDocumentosAnexoContratoView>
    {
        readonly ISfContratosManagementServices _contratoService;
        readonly ISfDocumentosAnexoContratoManagementServices _anexosService;
        readonly ISfTBL_Admin_OptionListManagementServices _optionListService;
        readonly ISfLogContratosManagementServices _log;

        public AdminDocumentosAnexoContratoPresenter(ISfContratosManagementServices contratoService,
                                                ISfDocumentosAnexoContratoManagementServices anexosService,
                                                ISfTBL_Admin_OptionListManagementServices optionListService,
                                                ISfLogContratosManagementServices log)
        {
            _contratoService = contratoService;
            _anexosService = anexosService;
            _optionListService = optionListService;
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
            View.CanAddDocumentos = View.UserSession.IsInRole("Administrador") || View.UserSession.IsInRole("Correspondencia");
            LoadCategorias();
            LoadAnexos();
        }

        void InitView()
        {
        }

        public void LoadCategorias()
        {
            var items = new List<DTO_ValueKey>();
            try
            {
                var op = _optionListService.ObtenerOpcionBykeyModuleId("CategoriaDocumentos", Convert.ToInt32(View.IdModule));

                if (op != null)
                {
                    var splitValue = op.Value.Split('|');

                    foreach (var s in splitValue)
                    {
                        items.Add(new DTO_ValueKey() { Id = s, Value = s });
                    }
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }

            View.LoadCategorias(items);
        }              
        
        public void LoadAnexos()
        {
            if (string.IsNullOrEmpty(View.IdContrato)) return;
            try
            {
                var items = _anexosService.GetAnexosByContratoCategoria(Convert.ToInt32(View.IdContrato), View.Categoria);
                View.LoadAnexos(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public DocumentosAnexoContrato GetAnexoDoumento(Guid id)
        {
            try
            {
                var documento = _anexosService.GetById(id);

                return documento;
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }

            return null;
        }

        public void SaveDocumento()
        {
            if (string.IsNullOrEmpty(View.IdContrato)) return;

            try
            {
                var contrato = _contratoService.FindById(Convert.ToInt32(View.IdContrato));
                var model = GetModel();

                _anexosService.Add(model);

                var log = GetLog();
                log.Descripcion = string.Format("El usuario [{0}], ha adicionado un documento para la categoría [{1}], con titulo [{2}]."
                                                , View.UserSession.Nombres
                                                , View.CategoriaDocumento
                                                , View.Titulo);
                _log.Add(log);

                contrato.ModifiedBy = View.UserSession.IdUser;
                contrato.ModifiedOn = log.CreateOn.GetValueOrDefault();

                _contratoService.Modify(contrato);

                LoadAnexos();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        DocumentosAnexoContrato GetModel()
        {
            var model = new DocumentosAnexoContrato();

            model.IdDocumentoContrato = Guid.NewGuid();
            model.IdContrato = Convert.ToInt32(View.IdContrato);
            model.Titulo = View.Titulo;
            model.Descripcion = View.Descripcion;
            model.Categoria = View.CategoriaDocumento;
            model.NombreArchivo = View.NombreArchivo;
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