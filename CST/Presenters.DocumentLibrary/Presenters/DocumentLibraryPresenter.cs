using System;
using System.Collections.Generic;
using System.Reflection;
using Applcations.MainModule.DocumentLibrary.IServices;
using Application.Core;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.DocumentLibrary.IViews;

namespace Presenters.DocumentLibrary.Presenters
{
    public class DocumentLibraryPresenter : Presenter<IDocumentLibraryView>
    {
        private readonly ISfTBL_ModuloDocumentosAnexos_CarpetasManagementServices _foldersservices;
        private readonly ISfTBL_ModuloDocumentosAnexos_DocumentoManagementServices _documentServices;

        public DocumentLibraryPresenter(
            ISfTBL_ModuloDocumentosAnexos_CarpetasManagementServices foldersservices,
            ISfTBL_ModuloDocumentosAnexos_DocumentoManagementServices documentServices)
        {
            _foldersservices = foldersservices;
            _documentServices = documentServices;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.GetFilesEvent += ViewGetFilesEvent;
            View.FilterEvent += ViewFilterEvent;
            View.GetFoldersEvent += ViewGetFoldersEvent;
            View.DownloadEvent += ViewDownloadEvent;
            View.DeleteEvent += ViewDeleteEvent;
            View.DeleteFolderEvent += ViewDeleteFolderEvent;
        }

        void ViewDeleteFolderEvent(object sender, EventArgs e)
        {
            DeleteFolderAndFiles();
        }

        void ViewDeleteEvent(object sender, EventArgs e)
        {
            if(sender==null)return;
            var parameters = (Dictionary<string, string>) sender;
            Delete(parameters);
        }

        void ViewDownloadEvent(object sender, EventArgs e)
        {
            if (sender == null) return;
            DownloadFile(Convert.ToInt32(sender));
        }

        void ViewGetFoldersEvent(object sender, EventArgs e)
        {
            GetFoldersByIdContrato();
        }

        void ViewFilterEvent(object sender, EventArgs e)
        {
            ListadoDocumentosPorCarpeta(sender == null ? 0 : Convert.ToInt32(sender));
        }

        void ViewGetFilesEvent(object sender, EventArgs e)
        {
            ListadoDocumentosPorCarpeta(0);
        }

        void ViewLoad(object sender, EventArgs e)
        {
            GetFoldersByIdContrato();
        }


        private void GetFoldersByIdContrato()
        {
            try
            {
                if (string.IsNullOrEmpty(View.IdContrato)) return;
                var list = _foldersservices.GetFoldersByIdContrato(View.IdContrato);
                View.FolderList(list);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Listado Carpetas"), TypeError.Error));
            }
        }

        private void ListadoDocumentosPorCarpeta(int currentFile)
        {
            try
            {
                if (string.IsNullOrEmpty(View.IdFolder)) return;
                var idFolder = Convert.ToInt32(View.IdFolder);

                var total = _documentServices.CountByIdFolder(idFolder, View.NameFile);
                View.TotalRegistrosPaginador = total == 0 ? 1 : total;

                var list = _documentServices.FindByIdFolder(idFolder, View.NameFile, currentFile, View.PageSize);
                View.DocumentList(list);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Listado Archivos"), TypeError.Error));
            }

        }

        private void DeleteFolderAndFiles()
        {
            if (string.IsNullOrEmpty(View.IdFolder)) return;
            _foldersservices.DeleteFolderAndFiles(Convert.ToInt32(View.IdFolder));
            GetFoldersByIdContrato();
            ListadoDocumentosPorCarpeta(0);
        }

        private void DownloadFile(int idDocument)
        {
            var doc = _documentServices.FindById(idDocument);
            if (doc == null) return;
            View.DownloadFile(doc.Adjunto, doc.Nombre);
        }


        private void Delete(Dictionary<string, string> parameters)
        {
            try
            {
                _documentServices.DeleteDocumentAndContent(parameters);
                ListadoDocumentosPorCarpeta(0);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Listado Archivos"), TypeError.Error));
            }
        }
    }
}