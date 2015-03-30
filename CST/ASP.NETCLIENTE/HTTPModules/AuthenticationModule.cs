using System;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Security;
using Applications.MainModule.Admin.IServices;
using ASP.NETCLIENTE.Utils;
using Infraestructure.CrossCutting.Security.Security;
using Infrastructure.CrossCutting;
using Infrastructure.CrossCutting.IoC;
using Infrastructure.CrossCutting.Logging;
using Infrastructure.CrossCutting.NetFramework.Util;

namespace ASP.NETCLIENTE.HTTPModules
{
    public class AuthenticationModule : IHttpModule
    {
        private ITraceManager _traceManager;
        private ISfTBL_Admin_UsuariosManagementServices _iAutentication;
        ISfTBL_Admin_OptionListManagementServices _optionListService;

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += ContextAuthenticateRequest;
            //context.AuthorizeRequest += OnAuthorizeRequest;
            _iAutentication = IoC.Resolve<ISfTBL_Admin_UsuariosManagementServices>();
            _traceManager = IoC.Resolve<ITraceManager>();
            _optionListService = IoC.Resolve<ISfTBL_Admin_OptionListManagementServices>();
        }

        //void OnAuthorizeRequest(object sender, EventArgs args)
        //{
        //    var app = (HttpApplication)sender;
        //    var context = app.Context;
        //    if (context.SkipAuthorization)
        //        return;

        //    if (!context.Request.FilePath.EndsWith(".aspx") )
        //        return;

        //    if (context.Request.HttpMethod == "GET")
        //    {
        //        if (HttpContext.Current.User != null)
        //        {
        //            //Si el usuario esta Autenticado 
        //            if (HttpContext.Current.User.Identity.IsAuthenticated)
        //            {
        //                if (HttpContext.Current.User is SolutionFrameworkPrincipal)
        //                {
        //                    //Se verifica si el Perfil del usuario tiene autorización para acceder a la página 
        //                    if (!_iAutentication.ValidarAutorizacion(
        //                            HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath))
        //                        HttpContext.Current.Server.Transfer("~/FrmError.aspx?error=401");
        //                }
        //            }
        //        }
        //    }
        //}


        private void ContextAuthenticateRequest(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;

            if (app.Context.User != null && app.Context.User.Identity.IsAuthenticated)
            {
                //_traceManager.LogInfo("ContextAuthenticateRequest : " + app.Context.User.Identity.Name,
                //                        LogType.Notify);
                if (string.IsNullOrEmpty(app.Context.User.Identity.Name)) return;
                if (IsNumeric(app.Context.User.Identity.Name))
                {
                    var userId = app.Context.User.Identity.Name;
                    //var userId = app.Context.User.Identity.Name;
                    var solutionFrameworkUser = _iAutentication.FindById(Convert.ToInt32(userId));
                    if (solutionFrameworkUser != null)
                    {
                        solutionFrameworkUser.IsAuthenticated = true;
                        app.Context.User = new SolutionFrameworkPrincipal(solutionFrameworkUser);
                    }
                    else
                    {
                        FormsAuthentication.SignOut();
                        HttpContext.Current.Server.Transfer("~/FrmError.aspx?error=402");
                    }
                }
            }

            if (!app.Request.AppRelativeCurrentExecutionFilePath.Contains("FrmError"))
                CheckEncryption();
        }

        void CheckEncryption()
        {
            var license = string.Format(@"{0}\license.key", HttpContext.Current.Server.MapPath("~"));
            
            if (!File.Exists(license))
            {
                HttpContext.Current.Response.Redirect("~/FrmError.aspx?error=101");
            }

            var client = GetClient();
            var mac = MacHelper.GetMacAddress();
            var clientCrypt = string.Empty;
            var macCrypt = string.Empty;

            using (StreamReader sw = new StreamReader(license))
            {
                clientCrypt = sw.ReadLine();
                macCrypt = sw.ReadLine();
            }

            try
            {
                var clientDecrypt = StringEncryption.DecryptString(clientCrypt, mac);
                var macDecrypt = StringEncryption.DecryptString(macCrypt, mac);

                if (mac == macDecrypt && clientDecrypt == client)
                {
                    Console.WriteLine("Si aplica");
                }
                else
                {
                    throw new Exception("Las llaves de registro de aplicación no coinciden");
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Redirect("~/FrmError.aspx?error=100");
            }
        }

        string GetClient()
        {
            var op = _optionListService.ObtenerOpcionBykey("ClientName");

            if (op != null)
            {
                return op.Value;
            }

            return "solver";
        }

        private static bool IsNumeric(object val)
        {
            try
            {
                Convert.ToInt32(val);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AuthenticateUser(string username, string password)
        {

            try
            {

                var user = _iAutentication.GetUserByCredential(username.Trim(), password.Trim());
                if (user != null)
                {
                    if (!user.IsActive)
                    {
                        _traceManager.LogInfo(string.Format(CultureInfo.InvariantCulture,
                                        "El usuario {0} intento ingresar estando inactivo.",
                                        username),
                                        LogType.Notify);
                        HttpContext.Current.Server.Transfer("~/FrmError.aspx?error=402");
                    }
                    user.IsAuthenticated = true;
                    var currentIp = HttpContext.Current.Request.UserHostAddress;
                    user.lastlogin = DateTime.Now;
                    user.lastip = currentIp;
                    // Save login date and IP
                    _iAutentication.Modify(user);
                    // Create the authentication ticket
                    HttpContext.Current.User = new SolutionFrameworkPrincipal(user);
                    FormsAuthentication.SetAuthCookie(user.IdUser.ToString(), false);

                    return true;
                }
                _traceManager.LogInfo(String.Format("Nombre de Usuario no válido: {0}.", username), LogType.Notify);
                return false;
            }
            catch (Exception ex)
            {
                _traceManager.LogInfo(String.Format("Error Técnico Modulo de Autenticación:  '{0}': " + ex.Message), LogType.Notify);
                return false;
            }

           
        }

        public bool AuthenticateUser(string codigo)
        {

            try
            {
                //var userService = IoC.Resolve<ISfTBL_Maestra_StaffManagementServices>();
                //var user = userService.GetUserByIdStaff(codigo);
                //if (user != null)
                //{
                //    if (!user.IsActive)
                //    {
                //        _traceManager.LogInfo(string.Format(CultureInfo.InvariantCulture,
                //                        "El usuario {0} intento ingresar estando inactivo.",
                //                        user.Nombres),
                //                        LogType.Notify);
                //        HttpContext.Current.Server.Transfer("~/FrmError.aspx?error=402");
                //    }
                //    user.IsAuthenticated = true;
                //    var currentIp = HttpContext.Current.Request.UserHostAddress;
                //    user.lastlogin = DateTime.Now;
                //    user.lastip = currentIp;
                //    // Save login date and IP
                //    userService.Modify(user);
                //    // Create the authentication ticket
                //    HttpContext.Current.User = new SolutionFrameworkPrincipal(user);
                //    FormsAuthentication.SetAuthCookie(user.CodigoStaff, false);
                    return true;
                //}
                //_traceManager.LogInfo(String.Format("Código de Usuario no válido: {0}.", codigo), LogType.Notify);
                //return false;
            }
            catch (Exception ex)
            {
                _traceManager.LogInfo(String.Format("Error Técnico Modulo de Autenticación:  '{0}': " + ex.Message), LogType.Notify);
                return false;
            }

            //return true;
        }

        /// <summary>
        /// Log out the current user.
        /// </summary>
        public void Logout()
        {
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }
        }

        public void Dispose()
        {
           
        }
    }
}