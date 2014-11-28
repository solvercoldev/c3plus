using System;
using Application.Core;

namespace Presenters.DocumentLibrary.IViews
{
    public interface INewFolderView : IViewUc
    {

        event EventHandler SaveEvent;

        string IdParent { get; }

        string NombreFolder { get; }

        string IdContrato { get; }
    }
}