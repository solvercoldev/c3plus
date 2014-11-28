
using System;
using System.Web;
using System.Web.UI;
using ASP.NETCLIENTE.HTTPModules;
using Domain.MainModules.Entities;
using Infraestructure.CrossCutting.Security.IServices;
using Infrastructure.CrossCutting;
using Infrastructure.CrossCutting.IoC;
using Infrastructure.CrossCutting.NetFramework.Enums;
using log4net;
using Microsoft.Practices.Unity;
using Modules.Loader;

namespace ASP.NETCLIENTE.UI
{
    public class SolutionFrameworkPage : Page
    {
        private const string ApplicationName = "C3+";
        private static readonly ILog Logger = LogManager.GetLogger(ApplicationName);
        private readonly ModuleLoader _moduleLoader;
        private readonly IUnityContainer _container;
        private readonly ISectionServices _sectionServices;
        private TBL_Admin_Modulos _modulo;

        protected  ModuleLoader LoaderModule
        {
            get { return _moduleLoader; }
        }


        protected TBL_Admin_Modulos Modulo
        {
            get { return _modulo; }
        }

        protected IUnityContainer Container
        {
            get { return _container; }
        }

        protected SolutionFrameworkPage()
        {
            _container = IoC.Container;
            _moduleLoader = Container.Resolve<ModuleLoader>();
            _sectionServices = Container.Resolve<ISectionServices>();

        }

        protected override void OnInit(EventArgs e)
        {
           
                if (!string.IsNullOrEmpty(ModuleId))
                {
                    _modulo = _sectionServices.GetModuleById(ModuleId);
                }

                if (_modulo == null)
                {
                    var am = (AuthenticationModule) Context.ApplicationInstance.Modules["AuthenticationModule"];
                    am.Logout();
                    Context.Response.Redirect("~/Login.aspx");
                }
            

            base.OnInit(e);

        }


       
        protected string ModuleId
        {
            get { return Request.QueryString["ModuleId"]; }
        }


        #region Log

        protected static void LogError(string metodo, string user, Uri url, Exception ex)
        {
            SetOptionalParametersOnLogger(user, url);
            LogError(metodo, ex);
        }

        protected  void LogError(int idDocumento,int idUser, string userName, Acciones accion)
        {
            throw new NotImplementedException("Aun no se ha imlementado este metodo, es necesario generar un ILogService para la App.");
        }

        private static void SetOptionalParametersOnLogger(string user, Uri url)
        {
            if (user != null)
            {
                MDC.Set("user", user);
            }
            MDC.Set("url", url.ToString());
        }

        private static void LogError(string message, Exception ex)
        {
            if (ex == null) return;

            if (Logger.IsErrorEnabled)
            {
                Logger.Error(message, ex.InnerException ?? ex);
            }
        }

        public void LogInfo(string message, LogType typeLog)
        {
            ILog logger = null;
            logger = LogManager.GetLogger(typeLog == LogType.Notify
                                          ? LogType.Notify.ToString()
                                          : LogType.General.ToString());

            if (logger.IsInfoEnabled)
            {
                logger.Info(message);
            }
        }
        #endregion
    }
}