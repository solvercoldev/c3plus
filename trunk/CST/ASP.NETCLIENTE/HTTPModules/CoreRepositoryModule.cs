using System;
using System.Web;
using Infrastructure.CrossCutting.IoC;
using Infrastructure.CrossCutting.Logging;

namespace ASP.NETCLIENTE.HTTPModules
{
    public class CoreRepositoryModule : IHttpModule
    {
        private ITraceManager _traceManager; 
        public void Init(HttpApplication context)
        {
            context.BeginRequest += ContextBeginRequest;
            context.EndRequest += ContextEndRequest;
            _traceManager = IoC.Resolve<ITraceManager>();
        }


        private void ContextBeginRequest(object sender, EventArgs e)
        {
            var context = ((HttpApplication)sender).Context;
            context.Items["RequestStart"] = DateTime.Now;
        }

        private void ContextEndRequest(object sender, EventArgs e)
        {
           // Log duration
            var context = ((HttpApplication)sender).Context;
            var rawUrl = context.Request.RawUrl;
            var startTime = (DateTime)context.Items["RequestStart"];
            var duration = DateTime.Now - startTime;
            //_traceManager.LogInfo(string.Format(CultureInfo.InvariantCulture,
            //                            "Solicitud Finalizada para el recurso [{0}]. Duración Total: {1} ms.",
            //                            rawUrl,
            //                            duration.Milliseconds),
            //                            LogType.Notify);
        }

        public void Dispose()
        {
           
        }
    }
}