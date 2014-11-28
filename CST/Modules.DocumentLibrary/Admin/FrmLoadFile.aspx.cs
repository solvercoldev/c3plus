using System;
using System.IO;
using System.Web.UI;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.DocumentLibrary.IViews;
using Presenters.DocumentLibrary.Presenters;

namespace Modules.DocumentLibrary.Admin
{
    public partial class FrmLoadFile :Page// ViewPage<LoadFilePresenter, ILoadFileView>, ILoadFileView
    {
        public event EventHandler SaveEvent;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OkButtonClick(object sender, EventArgs e)
        {
            if (SaveEvent != null)
                SaveEvent(null, EventArgs.Empty);
        }


        //public TBL_Admin_Usuarios UserSession
        //{
        //    get { return AuthenticatedUser; }
        //}

        //public string IdModule
        //{
        //    get { return ModuleId; }
        //}

        public byte[] Attachments
        {
            get { return fuSingleFile.FileBytes; }
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

        public string IdFolder
        {
            get { return Request.QueryString["IdFolder"]; }
        }

        public string Comentarios
        {
            get { return string.Empty; }
            set { throw new NotImplementedException(); }
        }

        public string ContentTypeFile
        {
            get { return fuSingleFile.PostedFile.ContentType;     }
        }

        public string TipoArchivo
        {
            get { throw new NotImplementedException(); }
        }

        public void ListadoTipos(string[] items)
        {
            throw new NotImplementedException();
        }
    }
}