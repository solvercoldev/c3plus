using System;
using Application.Core;

namespace Presenters.DocumentLibrary.IViews
{
    public interface ILoadFileView : IView
    {
        #region Events

        event EventHandler SaveEvent;

        #endregion

        #region Members

        Byte[] Attachments { get; }

        string NameFile { get; }

        string IdFolder { get; }

        string Comentarios { get; }

        string ContentTypeFile { get; }

        string TipoArchivo { get; }

        void ListadoTipos(string[] items);

        #endregion


    }
}