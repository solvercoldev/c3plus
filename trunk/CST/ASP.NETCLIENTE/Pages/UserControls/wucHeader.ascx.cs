using System;
using ASP.NETCLIENTE.Utils;

namespace ASP.NETCLIENTE.Pages.UserControls
{
    public partial class wucHeader : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {           
            lblLoginName.Text = this.Page.User.Identity.Name;
            //lblVersion.Text = string.Format("Versión: {0}", WebAssemblyHelper.getApplicationAssembly().GetName().Version);
        }

        protected void LoginStatusOnLoggedOut(object sender, EventArgs e)
        {
            Session.Abandon();
        }
    }
}