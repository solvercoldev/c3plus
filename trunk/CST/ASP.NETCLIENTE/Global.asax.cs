using System;
using System.Web;
using Infrastructure.CrossCutting.IoC;
using log4net;
using log4net.Config;
using Microsoft.Practices.Unity;
using Modules.Loader;
using ASP.NETCLIENTE.Utils;
using System.IO;
using Infrastructure.CrossCutting.NetFramework.Util;

namespace ASP.NETCLIENTE
{
    public class Global : HttpApplication
    {

        private static readonly ILog Log = LogManager.GetLogger(typeof(Global));
        private const string ErrorPageLocation = "~/Error.aspx";

        /// <summary>
        /// Obteniendo el contenedor
        /// </summary>
        private static IUnityContainer Container
        {
            get { return IoC.Container; }   
        }

        void Application_Start(object sender, EventArgs e)
        {
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application["IsFirstRequest"] = true;
            HttpContext.Current.Application["ModulesLoaded"] = false;
            HttpContext.Current.Application["IsModuleLoading"] = false;
            HttpContext.Current.Application["IsInstalling"] = false;
            HttpContext.Current.Application["IsUpgrading"] = false;
            HttpContext.Current.Application.UnLock();

            //Inicializando log 4Net.
            XmlConfigurator.Configure();
            IoCFactory.InitializeContainer();
            IoCFactory.RegisterModuleLoader();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Bootstrap Cuyahoga at the first request. We can't do this in Application_Start because
            // we need the HttpContext.Response object to perform redirect. In IIS 7 integrated mode, the 
            // Response isn't available in Application_Start.
            if ((bool)HttpContext.Current.Application["IsFirstRequest"])
            {

                HttpContext.Current.Application.Lock();
                HttpContext.Current.Application["IsFirstRequest"] = false;
                HttpContext.Current.Application.UnLock();
            }

            // Load active modules. This can't be done in Application_Start because the Installer might kick in
            // before modules are loaded.
            if (!(bool)HttpContext.Current.Application["ModulesLoaded"]
                && !(bool)HttpContext.Current.Application["IsModuleLoading"]
                && !(bool)HttpContext.Current.Application["IsInstalling"]
                && !(bool)HttpContext.Current.Application["IsUpgrading"])
            {
                LoadModules();
            }
        }

        private static void LoadModules()
        {
            if (Log.IsDebugEnabled)
            {
                Log.Debug("Entering module loading.");
            }
            // Load module types into the container.
            var loader = Container.Resolve<ModuleLoader>();
            loader.RegisterActivatedModules();

            if (Log.IsDebugEnabled)
            {
                Log.Debug("Finished module loading. Now redirecting to self.");
            }
            // Re-load the requested page (to avoid conflicts with first-time configured NHibernate modules )
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl);

        }

        void Application_End(object sender, EventArgs e)
        {
            Container.Dispose();
        }

        void Application_Error(object sender, EventArgs e)
        {
            if (Context != null && Context.IsCustomErrorEnabled)
            {
                Server.Transfer(ErrorPageLocation, false);
            }
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }
      
        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }       
    }
}
