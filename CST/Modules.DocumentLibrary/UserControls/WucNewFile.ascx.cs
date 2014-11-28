using System;
using System.IO;
using System.Web.UI.WebControls;
using Application.Core;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.DocumentLibrary.IViews;
using Presenters.DocumentLibrary.Presenters;

namespace Modules.DocumentLibrary.UserControls
{
    public partial class WucNewFile :   ViewUserControl<LoadFilePresenter, ILoadFileView>, ILoadFileView
    {
        public event EventHandler SaveEvent;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string IdFolder
        {
            get { return FolderId; }
            
        }

        public string Comentarios
        {
            get { return txtComentarios.Text; }
        }

        public string ContentTypeFile
        {
            get
            {
                return fuSingleFile.HasFile ? fuSingleFile.PostedFile.ContentType : string.Empty;
            }
        }

        public string TipoArchivo
        {
            get { return ddlTipo.SelectedValue; }
        }

        public void ListadoTipos(string[] items)
        {
            ddlTipo.Items.Clear();
            foreach (var item in items)
            {
                ddlTipo.Items.Add(new ListItem(item,item));
            }
        }

        public byte[] Attachments
        {
            get
            {
                return fuSingleFile.FileBytes;
            }
        }

        public string NameFile
        {
            get
            {
                var fileName = "";
                if (fuSingleFile.HasFile)
                {
                    var fi = new FileInfo(fuSingleFile.PostedFile.FileName);
                    fileName = fi.Name;
                }
                return fileName;
            }

        }

        protected void OkButtonClick(object sender, EventArgs e)
        {
            if (SaveEvent != null)
                SaveEvent(null, EventArgs.Empty);

            InvokeActualizarEvent(new ViewResulteventArgs(null));
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

    }
}