using System;
using System.Reflection;
using Applcations.MainModule.DocumentLibrary.IServices;
using Application.Core;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.DocumentLibrary.IViews;

namespace Presenters.DocumentLibrary.Presenters
{
    public class LoadFilePresenter : Presenter<ILoadFileView>
    {
        private readonly ISfTBL_ModuloDocumentosAnexos_DocumentoManagementServices _docServices;
        private readonly ISfTBL_Admin_OptionListManagementServices _optionsServices;
        public LoadFilePresenter(
            ISfTBL_ModuloDocumentosAnexos_DocumentoManagementServices docServices, 
            ISfTBL_Admin_OptionListManagementServices optionsServices)
        {
            _docServices = docServices;
            _optionsServices = optionsServices;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.SaveEvent += ViewSaveEvent;
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
            SaveDocument();
        }

        void ViewLoad(object sender, EventArgs e)
        {
            ListadoTiposDocumentos();
        }

        private void SaveDocument()
        {
            try
            {
                if (string.IsNullOrEmpty(View.IdFolder)) return;
                if (View.Attachments.Length == 0) return;

                _docServices.SaveDocument(Convert.ToInt32(View.IdFolder), View.UserSession,
                                          View.NameFile, View.Comentarios, View.Attachments, View.ContentTypeFile, View.TipoArchivo);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }


        private void ListadoTiposDocumentos()
        {
            try
            {
                var op = _optionsServices.ObtenerOpcionBykey("ListaDocumentos");
                if(op == null)return;
                var listado = op.Value.Split('|');
                View.ListadoTipos(listado);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}