using System;
using Application.Core;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.DocumentLibrary.IViews;
using Presenters.DocumentLibrary.Presenters;

namespace Modules.DocumentLibrary.UserControls
{
    public partial class WucNewFolder : ViewUserControl<NewFolderPresenter, INewFolderView>, INewFolderView
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtNombreCarpeta.Focus();
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public event EventHandler SaveEvent;

        public string IdParent
        {
            get { return FolderId; }
        }

       

        public string NombreFolder
        {
            get { return txtNombreCarpeta.Text; }
        }

        public string IdContrato
        {
            get { return Request.QueryString["IdContrato"]; }
        }
       
        protected void OkButtonClick(object sender, EventArgs e)
        {
            if (SaveEvent != null)
                SaveEvent(null, EventArgs.Empty);

            InvokeActualizarEvent(new ViewResulteventArgs(null));
        }
    }
}