using System;
using System.Web.UI;

namespace ASP.NETCLIENTE.Pages.UserControls
{
    public partial class wucMenuBar : System.Web.UI.UserControl
    {
        public event EventHandler Click;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Page.IsPostBack)return;
            btnNew.Visible = ShowNew;
            btnSave.Visible = ShowSave;
            btnDelete.Visible = ShowDelete;
            btnClose.Visible = ShowClose;
        }

        #region Members

        protected void OnClick(EventArgs e)
        {
            if (Click != null)
            {
                Click(this, e);
            }
        }

        public bool ShowNew { get; set; }

        public bool ShowSave { get; set; }

        public bool ShowDelete { get; set; }

        public bool ShowClose { get; set; }


        #endregion

        #region Events

        protected void BtnNewClick(object sender, ImageClickEventArgs e)
        {
            Click("New", e);
        }

        protected void BtnSaveClick(object sender, ImageClickEventArgs e)
        {
            Click("Save", e);
        }

        protected void BtnDeleteClick(object sender, ImageClickEventArgs e)
        {
            Click("Delete", e);
        }

        protected void BtnCloseClick(object sender, ImageClickEventArgs e)
        {
            Click("Close", e);
        }


        #endregion
    }
}