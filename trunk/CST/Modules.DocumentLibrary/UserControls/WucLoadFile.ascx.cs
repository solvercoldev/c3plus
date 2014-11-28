using System;
using System.Web.UI;
using ASP.NETCLIENTE.UI;

namespace Modules.DocumentLibrary.UserControls
{
    public partial class WucLoadFile : UserControl
    {
        public event EventHandler RefreshListEvent;

        private void InvokeRefreshListEvent(EventArgs e)
        {
            EventHandler handler = RefreshListEvent;
            if (handler != null) handler(this, e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadUserControlVentanaMensajes();
        }

        public void OpenWindow(string  wucLoad)
        {
            LastLoadedControlMessages = wucLoad;
            LoadUserControlVentanaMensajes();
            mpeLoadFileDl.Show();
        }

        private string LastLoadedControlMessages
        {
            get
            {
                return ViewState["LastLoadedControlMessages"] as string;
            }
            set
            {
                ViewState["LastLoadedControlMessages"] = value;
            }
        }


        private void LoadUserControlVentanaMensajes()
        {
            var controlPath = LastLoadedControlMessages;

            if (string.IsNullOrEmpty(controlPath))
            {
                controlPath = "WucNewFile.ascx";
            }
            if (string.IsNullOrEmpty(controlPath)) return;
            phloadControlLoadFile.Controls.Clear();
            var uc = LoadControl(controlPath);
            uc.ID = controlPath.Split('.')[0];
            ConfigurarUserControl(uc);
            phloadControlLoadFile.Controls.Add(uc);
        }

        private void ConfigurarUserControl(Control oControl)
        {
            var uc = (BaseUserControl)oControl;
            if (uc == null) return;
            uc.FolderId = IdFolder;
            uc.ActualizarEvent += UcActualizarEvent;      
        }

        void UcActualizarEvent(object sender, Application.Core.ViewResulteventArgs e)
        {
            InvokeRefreshListEvent(e);
        }

        public string IdFolder
        {
            get { return ViewState["IdFolder"] == null ? string.Empty : ViewState["IdFolder"].ToString(); }
            set { ViewState["IdFolder"] = value; }
        }
    }
}