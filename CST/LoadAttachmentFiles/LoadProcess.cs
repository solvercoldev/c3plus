using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Microsoft.Practices.Unity;
using Infrastructure.CrossCutting.IoC;
using Application.MainModule.SqlServices.IServices;
using Application.MainModule.Contratos.IServices;
using Domain.MainModules.Entities;
using System.IO;
using Infrastructure.CrossCutting.Logging;
using Infrastructure.CrossCutting;

namespace LoadAttachmentFiles
{
    public class LoadProcess
    {
        string RootPath = ConfigurationManager.AppSettings.Get("RootPath");

        static LoadProcess _instance;
        public static LoadProcess Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LoadProcess();

                return _instance;
            }
        }

        private static IUnityContainer Container
        {
            get { return IoC.Container; }
        }

        readonly ISfContratosManagementServices _contratoService;
        readonly ISfDocumentosAnexoContratoManagementServices _anexosService;
        readonly ISfDocumentosRadicadoManagementServices _documentoRadicadoService;
        readonly ITraceManager _traceManager;

        AdoHelper _adoHelper;

        public LoadProcess()
        {
            _contratoService = Container.Resolve<ISfContratosManagementServices>();
            _anexosService = Container.Resolve<ISfDocumentosAnexoContratoManagementServices>();
            _documentoRadicadoService = Container.Resolve<ISfDocumentosRadicadoManagementServices>();
            _traceManager = Container.Resolve<ITraceManager>();
            _adoHelper = new AdoHelper();
        }

        public void ProcessDirectory()
        {
            var rootDirectory = new DirectoryInfo(RootPath);

            foreach (var dir in rootDirectory.GetDirectories())
            {
                var id = Convert.ToInt32(dir.Name);

                var dtContrato = _adoHelper.GetInfoContratoByIdContratoMig(id);

                if (dtContrato.Rows.Count > 0)
                {
                    var contrato = _contratoService.GetContratoWithNavsById(Convert.ToInt32(string.Format("{0}", dtContrato.Rows[0]["IdContrato"])));

                    if (contrato != null)
                    {
                        var pathDocs = System.IO.Path.Combine(dir.FullName, "Docs");
                        var pathRE = System.IO.Path.Combine(dir.FullName, "RE");
                        var pathRS = System.IO.Path.Combine(dir.FullName, "RS");

                        var docDirectoryInfo = new DirectoryInfo(pathDocs);
                        var reDirectoryInfo = new DirectoryInfo(pathRE);
                        var rsDirectoryInfo = new DirectoryInfo(pathRS);

                        // Cargando Documentos Anexos
                        foreach (var anxContrato in docDirectoryInfo.GetFiles())
                        {
                            var docAnexoContrato = GetModel(contrato.IdContrato, anxContrato.Name, "Anexo Contrato", "Migración Anexos Contrato", File.ReadAllBytes(anxContrato.FullName));

                            try
                            {
                                _anexosService.Add(docAnexoContrato);
                            }
                            catch (Exception ex)
                            {
                                _traceManager.LogInfo(string.Format("Error al adicionar archivo de contrato,Contrato: {0}, Archivo: {1}, Error: {2}",
                                    contrato.IdContrato, anxContrato.FullName,
                                    ex.InnerException == null ? ex.Message : ex.InnerException.Message), LogType.Notify);
                            }
                        }

                        // Cargando Documentos Anexos RE Tipo 1
                        foreach (var anxRadicado in reDirectoryInfo.GetFiles())
                        {
                            var nombreRad = anxRadicado.Name.Remove(0, 33);

                            var dtRadicadoRE = _adoHelper.GetInfoRadicado(contrato.IdContrato, 1, nombreRad);

                            if (dtRadicadoRE.Rows.Count > 0)
                            {
                                var docRad = GetDocumentoModel(Convert.ToInt64(string.Format("{0}", dtRadicadoRE.Rows[0]["IdRadicado"]))
                                                             , nombreRad, File.ReadAllBytes(anxRadicado.FullName));

                                _documentoRadicadoService.Add(docRad);
                            }
                        }

                        // Cargando Documentos Anexos RE Tipo 2
                        foreach (var anxRadicado in rsDirectoryInfo.GetFiles())
                        {
                            var nombreRad = anxRadicado.Name.Remove(0, 33);

                            var dtRadicadoRS = _adoHelper.GetInfoRadicado(contrato.IdContrato, 2, nombreRad);

                            if (dtRadicadoRS.Rows.Count > 0)
                            {
                                var docRad = GetDocumentoModel(Convert.ToInt64(string.Format("{0}", dtRadicadoRS.Rows[0]["IdRadicado"]))
                                                             , nombreRad, File.ReadAllBytes(anxRadicado.FullName));

                                _documentoRadicadoService.Add(docRad);
                            }
                        }
                    }                    
                }                
            }
        }

        DocumentosAnexoContrato GetModel(int idContrato, string nombre, string titulo, string descripcion, byte[] anexo)
        {
            var model = new DocumentosAnexoContrato();

            model.IdDocumentoContrato = Guid.NewGuid();
            model.IdContrato = idContrato;
            model.Titulo = titulo;
            model.Descripcion = descripcion;
            model.Categoria = "Anexos";
            model.NombreArchivo = nombre;
            model.Archivo = anexo;
            model.IsActive = true;
            model.CreateBy = 1;
            model.CreateOn = DateTime.Now;
            model.ModifiedBy = 1;
            model.ModifiedOn = DateTime.Now;

            return model;
        }

        DocumentosRadicado GetDocumentoModel(long idRadicado, string nombre, byte[] anexo)
        {
            var model = new DocumentosRadicado();

            model.IdDocumentoRadicado = Guid.NewGuid();
            model.IdRadicado = idRadicado;
            model.NombreArchivo = nombre;
            model.Archivo = anexo;
            model.IsActive = true;
            model.CreateBy = 1;
            model.CreateOn = DateTime.Now;
            model.ModifiedBy = 1;
            model.ModifiedOn = DateTime.Now;

            return model;
        }
    }
}