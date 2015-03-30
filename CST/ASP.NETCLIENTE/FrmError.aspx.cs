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
                case "100": // Error de llave de registro de aplicacion
                    lblTituloError.Text = string.Format("Llave de Registro de Aplicación no concuerda.");
                    lblErrorCode.Text = string.Format("La llave del registro del sistema no concuerda con la instalada, por favor comuniquese con Solver para mas información.");
                    break;
                case "101": // Error de archivo de llave de registro de aplicacion no existe
                    lblTituloError.Text = string.Format("Archivo de registro de aplicación no encontrado");
                    lblErrorCode.Text = string.Format("El archivo de registro del sistema no se encuentra registrado en el servidor,por favor comuniquese con Solver para mas información.");
                    break;
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