using System;
using System.Collections;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.DocumentLibrary.IViews
{
    public interface IDocumentLibraryView : IViewUc
    {

        event EventHandler GetFilesEvent;
        event EventHandler GetFoldersEvent;
        event EventHandler FilterEvent;
        event EventHandler DownloadEvent;
        event EventHandler DeleteEvent;
        event EventHandler DeleteFolderEvent;

        string IdContrato { get; }

        string IdFolder { get; }

        string NameFile { get; }

        int PageSize { get; }

        int TotalRegistrosPaginador { set; }

        void FolderList(List<TBL_ModuloDocumentosAnexos_Carpetas> list);

        void DocumentList(List<TBL_ModuloDocumentosAnexos_Documento> list);

        void DownloadFile(byte[] adjunto, string nombre);

    }
}