using System;
using System.Web;
using System.Web.UI;
using Application.Core;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting;
using log4net;

namespace ASP.NETCLIENTE.UI
{
    public class BaseUserControl : UserControl, IBaseUserControl
    {

        private ModuleBase _module;

        #region eventos

        public event EventHandler<ViewResulteventArgs> ActualizarEvent;

        public void InvokeActualizarEvent(ViewResulteventArgs e)
        {
            var handler = ActualizarEvent;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<MessageBoxEventArgs> ViewResult;

        protected void InvokeViewResult(MessageBoxEventArgs e)
        {
            var handler = ViewResult;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<ViewResulteventArgs> ResponseeventHandler;

        public void InvokeResponseeventHandler(ViewResulteventArgs e)
        {
            var handler = ResponseeventHandler;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<ViewResulteventArgs> CloseWizardEvent;

        protected void InvokeCloseWizardEvent(ViewResulteventArgs e)
        {
            EventHandler<ViewResulteventArgs> handler = CloseWizardEvent;
            if (handler != null) handler(this, e);
        }

        #endregion

        #region Log

        private const string ApplicationName = "Empacor";

        private static readonly ILog Logger = LogManager.GetLogger(ApplicationName);

        protected void LogError(string metodo, string user, Uri url, Exception ex)
        {
            SetOptionalParametersOnLogger(user, url);
            SetLogError(metodo, ex);
        }

        protected void LogError(string metodo, Exception ex)
        {

            SetOptionalParametersOnLogger(AuthenticatedUser.Nombres, new Uri(GetUrl,UriKind.RelativeOrAbsolute));
            SetLogError(metodo, ex);
        }

        private static void SetOptionalParametersOnLogger(string user, Uri url)
        {
            if (user != null)
            {
                MDC.Set("user", user);
            }
            MDC.Set("url", url.ToString());
        }

        private static void SetLogError(string message, Exception ex)
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

        #region Members

        
        public ModuleBase Module
        {
            get { return _module; }
            set { _module = value; }
        }


        /// <summary>
        /// Lee el Parámetro ModuleId desde el QueryString
        /// </summary>
        protected string ModuleId
        {
            get { return Request.QueryString["ModuleId"]; }
        }

        protected static TBL_Admin_Usuarios AuthenticatedUser
        {
            get { return ((TBL_Admin_Usuarios)HttpContext.Current.User.Identity); }
        }

        private static string GetUrl
        {
            get { return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath; }
        }

        public bool IsLoadUserControl
        {
            get { return ViewState["IsLoadUserControl"] == null ? false : (bool)ViewState["IsLoadUserControl"]; }
            set { ViewState["IsLoadUserControl"] = value; }
        }

        public string IdDocument { get; set; }

        public string IdResponsable { get; set; }

        public string FolderId { get; set; }
        #endregion
    }
}