using System;
using System.Configuration;
using System.Data;
using System.Threading;
using Application.MainModule.Communication.IServices;
using Application.MainModule.SqlServices.IServices;
using Infrastructure.CrossCutting;
using Infrastructure.CrossCutting.IoC;
using Infrastructure.CrossCutting.Logging;
using Microsoft.Practices.Unity;

namespace Modules.NotifyApp
{
    public class NotifyManager
    {
        private static IUnityContainer Container
        {
            get { return IoC.Container; }
        }

        static NotifyManager _instance;
        public static NotifyManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NotifyManager();

                return _instance;
            }
        }

        readonly IContratosAdoService _contratoAdoService;
        readonly IContratoMailService _contratoMailService;
        readonly ITraceManager _traceManager;

        int _moduleId;
        string _baseUrl;

        public NotifyManager()
        {
            _contratoAdoService = Container.Resolve<IContratosAdoService>();
            _contratoMailService = Container.Resolve<IContratoMailService>();
            _traceManager = Container.Resolve<ITraceManager>();
            _moduleId = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ModuleId"));
            _baseUrl = ConfigurationManager.AppSettings.Get("BaseUrl");
        }

        public void NotifyPendingTask()
        {
            Console.WriteLine(string.Format("Inicio de proceso de notificacion."));
            // Obteniendo datos
            Console.WriteLine(string.Format("Obteniendo registros para notificar."));
            var dtCompromisos = _contratoAdoService.GetCompromisosToNotify();
            var dtRadicados = _contratoAdoService.GetRadicadosToNotify();

            // Enviando Compromisos
            foreach (DataRow drComp in dtCompromisos.Rows)
            {
                try
                {
                    Console.WriteLine(string.Format("Enviando Notificacion para Compromiso con ID: [{0}]", drComp["IdCompromiso"]));
                    SendCompromisoNotifyMail(drComp);
                }
                catch (Exception ex)
                {
                    _traceManager.LogInfo(string.Format("Error al enviar mail de notificación de alarma de compromiso.Cls:NotifyManager,Mtd:NotifyPendingTask, Error: {0}", ex.InnerException == null ? ex.Message : ex.InnerException.Message), LogType.Notify);
                }                
            }

            // Enviando Radicados
            foreach (DataRow drRad in dtRadicados.Rows)
            {
                try
                {
                    Console.WriteLine(string.Format("Enviando Notificacion para Radicado con ID: [{0}]", drRad["IdRadicado"]));
                    SendRadicadoNotifyMail(drRad);
                }
                catch (Exception ex)
                {
                    _traceManager.LogInfo(string.Format("Error al enviar mail de notificación de alarma de radicado.Cls:NotifyManager,Mtd:NotifyPendingTask, Error: {0}", ex.InnerException == null ? ex.Message : ex.InnerException.Message), LogType.Notify);
                }
            }

            Console.WriteLine(string.Format("Inicio de proceso de notificacion."));
        }

        void SendCompromisoNotifyMail(DataRow drComp)
        {
            object[] parameters = new object[3];
            parameters[0] = Convert.ToInt64(string.Format("{0}", drComp["IdCompromiso"]));
            parameters[1] = Convert.ToInt32(_moduleId);
            parameters[2] = _baseUrl;

            Thread mailThread = new Thread(_contratoMailService.SendCompromisoMailNotification);
            mailThread.Start(parameters);
        }

        void SendRadicadoNotifyMail(DataRow drRad)
        {
            object[] parameters = new object[3];
            parameters[0] = Convert.ToInt64(string.Format("{0}", drRad["IdRadicado"]));
            parameters[1] = Convert.ToInt32(_moduleId);
            parameters[2] = _baseUrl;

            Thread mailThread = new Thread(_contratoMailService.SendRadicadoMailNotification);
            mailThread.Start(parameters);
        }
    }
}