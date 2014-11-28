using Infrastructure.CrossCutting.Logging;
using System;
using log4net;

namespace Infrastructure.CrossCutting.NetFramework.Logging
{
   
    public sealed class TraceManager : ITraceManager
    {
        #region Members

        private const string ApplicationName = "Empacor";

        #endregion

        #region Private Methods

        public void LogError(string message, Exception ex)
        {
            var log = LogManager.GetLogger(ApplicationName);
            if(ex == null)return;
            
            if( log.IsErrorEnabled )
                log.Error(message,ex.InnerException);
        }

       
        public void LogError(string message,string user,Uri url, Exception ex)
        {
            SetOptionalParametersOnLogger(user, url);
            LogError(message, ex);
        }

        public void LogInfo(string message,LogType typeLog )
        {
            ILog logger = null;
            logger = LogManager.GetLogger(typeLog == LogType.Notify 
                                          ? LogType.Notify.ToString() 
                                          : LogType.General.ToString());

            if(logger.IsInfoEnabled)
            {
                logger.Info(message);
            }
        }

        public void LogWarining(string message, Exception ex)
        {
            var log = LogManager.GetLogger(ApplicationName);
            if (ex == null) return;

            if(log.IsWarnEnabled)
            {
                log.Warn(message,ex.InnerException);
            }
        }


        private static void SetOptionalParametersOnLogger(string user, Uri url)
        {
            if(user != null)
            {
                MDC.Set("User",user);
            }
            MDC.Set("Url", url.ToString());
        }

       
        #endregion


    }
}