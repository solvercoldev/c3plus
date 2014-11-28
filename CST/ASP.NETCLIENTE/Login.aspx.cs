using System;
using System.Web.Security;
using System.Web.UI;
using ASP.NETCLIENTE.HTTPModules;
using Infrastructure.CrossCutting;
using Infrastructure.CrossCutting.IoC;
using Infrastructure.CrossCutting.Logging;
using System.Configuration;

namespace ASP.NETCLIENTE
{
    public partial class Login : Page
    {
       
        private ITraceManager _traceManager;

        protected override void OnInit(EventArgs e)
        {
            _traceManager = IoC.Resolve<ITraceManager>();
            base.OnInit(e);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private void AutenticarUsuario()
        {
            try
            {
                var userWc = ConfigurationManager.AppSettings.Get("UsuarioAplicacion");
                var am = (AuthenticationModule)Context.ApplicationInstance.Modules["AuthenticationModule"];
                if (am == null)
                {
                    lblError.Text = @"Error de lectura del módulo de autenticación.";
                    return;
                }

                if (am.AuthenticateUser(txtUsername.Text,txtPassword.Text))
               // if (am.AuthenticateUser(userWc))
                {
                    Context.Response.Redirect(FormsAuthentication.GetRedirectUrl(User.Identity.Name, false));
                }
                else
                {
                    lblError.Text = @"Nombre de usuario o contraseña incorrecto.";
                    lblError.Visible = true;
                }

            }
            catch (Exception ex)
            {
                lblError.Text = @"Error Inesperado.";
                _traceManager.LogInfo(ex.Message,LogType.Notify);
            }
           
        }


        protected void BtnLoginClick(object sender, EventArgs e)
        {
            AutenticarUsuario();
        }
    }
}