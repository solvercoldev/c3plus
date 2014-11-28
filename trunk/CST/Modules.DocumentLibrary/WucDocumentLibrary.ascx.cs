using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Infragistics.Web.UI.NavigationControls;
using Presenters.DocumentLibrary.IViews;
using Presenters.DocumentLibrary.Presenters;
using ServerControls;

namespace Modules.DocumentLibrary
{
    public partial class WucDocumentLibrary : ViewUserControl<DocumentLibraryPresenter, IDocumentLibraryView>, IDocumentLibraryView
    {

        public event EventHandler GetFilesEvent;
        public event EventHandler GetFoldersEvent;
        public event EventHandler FilterEvent;
        public event EventHandler DownloadEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler DeleteFolderEvent;


        protected void Page_Load(object sender, EventArgs e)
        {
           imgDeleteFile.Attributes.Add("onclick","return confirm('¿Confirma que desea continuar?');");
        }

        protected override void OnInit(EventArgs e)
        {
            //wdtDocuments.NodeClick += WdtDocumentsNodeClick;
            rptDocuments.ItemCommand += RptDocumentsItemCommand;
            WucLoadFile1.RefreshListEvent += WucLoadFile1RefreshListEvent;
            RegistrarScript();
            base.OnInit(e);
        }

        void WucLoadFile1RefreshListEvent(object sender, EventArgs e)
        {

            if (FilterEvent != null)
                FilterEvent(null, EventArgs.Empty);

            if (GetFoldersEvent != null)
                GetFoldersEvent(null, EventArgs.Empty);
        }

        protected void RptDocumentsitemDataBound(object sender, RepeaterItemEventArgs e)
        {

            var doc = e.Item.DataItem as TBL_ModuloDocumentosAnexos_Documento;
            if (doc == null) return;

            ViewState[e.Item.UniqueID] = doc.IdDocumento.ToString();
            
            var imgDownload = e.Item.FindControl("imgDownload") as ImageButton;
            if (imgDownload != null)
            {
                imgDownload.CommandName = "Download";
                imgDownload.CommandArgument = doc.IdDocumento.ToString();
                imgDownload.ImageUrl = IconoPlantilla(doc.Nombre);
                RegistrarControlScriptManager(imgDownload);
            }

            var litFechaModificacion = e.Item.FindControl("litFechaModificacion") as Literal;
            if (litFechaModificacion != null)
            {
                litFechaModificacion.Text = doc.CreatedOn.HasValue ? doc.CreatedOn.GetValueOrDefault().ToShortDateString() : string.Empty;
            }

            //var litSize = e.Item.FindControl("litSize") as Literal;
            //if (litSize == null) return;
            //if (doc.Adjunto == null) return;
            //var size = Convert.ToDecimal(doc.Adjunto.LongLength);
            //litSize.Text = TamañoArchivo(size);

            var litDescripcion = e.Item.FindControl("litDescripcion") as Literal;
            if (litDescripcion != null)
                litDescripcion.Text = string.Format("<b>{0}</b>", doc.Comentarios);

            var chkSelect = e.Item.FindControl("chkSelect") as CheckBox;
            if (chkSelect != null)
            {
                chkSelect.Attributes.Add("idDoc",doc.IdDocumento.ToString());
            }

        }

        private static string TamañoArchivo(decimal lengthFile)
        {
            return (lengthFile / 1024) > 1024
                       ? Math.Round(((lengthFile / 1024) / 1024), 2) + " Mb"
                       : Math.Round((lengthFile / 1024), 2) + " Kb";
        }

        protected void PgrListadoPageChanged(object sender, PageChanged e)
        {
            if (FilterEvent != null)
                FilterEvent(e.CurrentPage, EventArgs.Empty);
        }

        protected void BtnDeleteClick(object sender, ImageClickEventArgs e)
        {
            if (DocumentsSelected.Count == 0)return;
            if (DeleteEvent != null)
                DeleteEvent(DocumentsSelected, EventArgs.Empty);
        }

        protected void ImgSerachClick(object sender, ImageClickEventArgs e)
        {
            if (FilterEvent != null)
                FilterEvent(null, EventArgs.Empty);
        }

        void RptDocumentsItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var id = e.CommandArgument.ToString();
            if (string.IsNullOrEmpty(id))
            {
                return;
            }

            if (DownloadEvent != null)
                DownloadEvent(id, EventArgs.Empty);
        }

        void WdtDocumentsNodeClick(object sender, DataTreeNodeClickEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Node.Value)) return;
            WucLoadFile1.IdFolder = e.Node.Value;
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

      
        public string IdContrato
        {
            get { return Request.QueryString["IdContrato"]; }
        }

        public string IdFolder
        {
            get { return hndNodeSelected.Value; }
        }

        public string NameFile
        {
            get { return txtSearchBox.Text; }
        }

        public int PageSize
        {
            get { return pgrListado.PageSize; }
        }

        public int TotalRegistrosPaginador
        {
            set { pgrListado.RowCount = value; }
        }

        public void FolderList(List<TBL_ModuloDocumentosAnexos_Carpetas> list)
        {
            wdtDocuments.Nodes.Clear();
            var root = new DataTreeNode
            {
                Text = "Document Library",
                Expanded = true,
                Key = "0",
                Value = "0",
                ImageUrl = "~/Resources/Images/folderdocument.png"
            };

            wdtDocuments.Nodes.Add(root);
            foreach (var folder in list)
            {
                var itemFile = new DataTreeNode
                {
                    Text = folder.Nombre,
                    Value = folder.IdFolder.ToString(),
                    Key = folder.IdFolder.ToString(),
                    ImageUrl = "~/Resources/Images/Proposalfolder.png"
                };

                root.Nodes.Add(itemFile);
            }
        }

        public void DocumentList(List<TBL_ModuloDocumentosAnexos_Documento> list)
        {
            rptDocuments.DataSource = list;
            rptDocuments.DataBind();
        }


        public void DownloadFile(byte[] adjunto, string nombre)
        {
            if (adjunto != null)
            {
                DownloadDocument(adjunto, nombre, ContenType(string.Format(".{0}", nombre.Split('.')[1])));
            }
        }

        protected void ContextMenuClick(object sender, DataMenuItemEventArgs e)
        {
            switch (e.Item.Key)
            {
                case "loadFile":
                    WucLoadFile1.IdFolder = hndNodeSelected.Value;
                    WucLoadFile1.OpenWindow("WucNewFile.ascx");
                    break;
                case "Select":
                    if (GetFilesEvent != null)
                        GetFilesEvent(hndNodeSelected.Value, EventArgs.Empty);
                    break;

                case "DeleteFolder":
                    if (DeleteFolderEvent != null)
                        DeleteFolderEvent(null, EventArgs.Empty);
                    break;

                case "NewFolder":
                    WucLoadFile1.IdFolder = hndNodeSelected.Value == "0" ? string.Empty : hndNodeSelected.Value;
                    WucLoadFile1.OpenWindow("WucNewFolder.ascx");
                    
                    break;
            }
           
        }

        private void RegistrarScript()
        {
            var strScript = " var lastNode = null; " +
                            " function MenuItem_Click(menu, eventArgs) { " +
                            " switch (eventArgs.getItem().get_key()) { " +
                            " case 'DeleteFolder': " +
                            "   if (lastNode != null  ) { " +
                            "     if(lastNode.get_key() == '') {" +
                            "       eventArgs.set_cancel(true); " +
                            "      }"+
                            "      else {"+
                            "       var r = confirm('¿Confirma que desea continuar?');" +
                            "       if(!r) {"+
                            "         eventArgs.set_cancel(true); " +
                            "       }"+
                            "     }"+
                            "   } " +
                            "   break; " +

                            " case 'NewFolder': " +
                            "   if (lastNode != null  ) { " +
                            "     if(lastNode.get_key() == '')" +
                            "       eventArgs.set_cancel(true); " +
                            "   } " +
                            "   break; " +

                            " case 'loadFile': " +
                            "   if (lastNode != null  ) { " +
                            "     if(lastNode.get_key() == '')" +
                            "       eventArgs.set_cancel(true); " +
                            "   } " +
                            "   break; " +

                            " case 'expand': " +
                            "   if (lastNode != null) {" +
                            "       lastNode.toggle(true, true); " +
                            "       eventArgs.set_cancel(true); " +
                            "  } break; " +
                            "   } " +
                            "}" +

                            " function Node_Click(tree, eventArgs) { " +
                            " lastNode = eventArgs.getNode(); " +
                            " var menu = $find('" + ContextMenu.ClientID + "'); " +
                            " var oHdnSelected = document.getElementById('" + hndNodeSelected.ClientID + "'); " +
                            " if (menu != null && eventArgs.get_browserEvent() != null && eventArgs.get_browserEvent().button == 2) {" +
                            "      oHdnSelected.value = eventArgs.getNode().get_key(); " +
                            "       menu.showAt(null, null, eventArgs.get_browserEvent()); " +
                            "    } " +
                            " }";

            var sm = ScriptManager.GetCurrent(Page);
            if (null != sm && sm.IsInAsyncPostBack)
                ScriptManager.RegisterStartupScript(this, GetType(), "customControlScript", strScript, true);
            else
                Page.ClientScript.RegisterStartupScript(GetType(), "customControlScript", strScript, true);
        }

        private Dictionary<string, string> DocumentsSelected
        {
            get { return GetDocumentSelect(); }
        }

        private Dictionary<string,string > GetDocumentSelect()
        {
            var selectList = new Dictionary<string, string>();

            foreach (RepeaterItem item in rptDocuments.Items)
            {
                var chkSelect = item.FindControl("chkSelect") as CheckBox;
                if(chkSelect == null)continue;
                if(chkSelect.Checked)
                {
                    var idContenido = chkSelect.Attributes["idDoc"];
                    var idDocumento = ViewState[item.UniqueID].ToString();
                    selectList.Add(idDocumento,idContenido);
                }
            }

            return selectList;
        }
    }
}