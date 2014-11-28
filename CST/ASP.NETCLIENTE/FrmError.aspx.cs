using System;
namespace ASP.NETCLIENTE
{
    public partial class FrmError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["error"] == null) return;
            switch (Request.QueryString["error"])
            {
                case "401":
                    lblErrorCode.Text = string.Format("Error de Acceso al Recurso Solicitado.");
                    lblTituloError.Text = string.Format("Acceso no Autorizado {0}", Request.QueryString["error"]);
                    break;

                case "402":
                    lblTituloError.Text = string.Format("Acceso no Autorizado");
                    lblErrorCode.Text = string.Format("El usuario se encuentra inactivo en del sistema.");
                    break;
            }

        }

        protected void BtnBackClick(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}