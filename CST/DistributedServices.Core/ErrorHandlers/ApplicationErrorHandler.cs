using System;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System.ServiceModel.Channels;

using DistributedServices.Core.Resources;
using Application.Core;
using Infrastructure.CrossCutting.Logging;
using Infrastructure.CrossCutting.NetFramework.Logging;

namespace DistributedServices.Core.ErrorHandlers
{
    /// <summary>
    /// 
    /// Default error handler for WCF Service Facade
    /// </summary>
    public sealed class ApplicationErrorHandler
        : IErrorHandler
    {
        /// <summary>
        /// Habilita el procesamiento error-relacionado y retorna un valor que indica si
        /// el despachador aborta la sesión y la instancia del contexto en ciertos casos
        /// </summary>
        /// <param name="error">La excepción arrojada durante el procesamiento</param>
        /// <returns>
        /// Verdadero si no se debe abortar la sesión (si hay una) y la instancia del contexto
        /// si no es de tipo System.ServiceModel.InstanceContextMode.Single; de lo
        /// contrario retorna falso. Por defecto es verdadero.
        /// </returns>
        public bool HandleError(Exception error)
        {
            if (error != null)
            {
                var traceManager = new TraceManager();
                traceManager.LogError(error.InnerException != null ? error.InnerException.Message : error.Message, error);
            }                

            return true;
        }

        /// <summary>
        /// Habilita la creación de un System.ServiceModel.FaultException{TDetail} personalizado
        /// que es retornado desde una excepción en el transcurso de un método del servicio
        /// </summary>
        /// <param name="error"> El objeto System.Exception arrojado en el transcurso de la operación del servicio</param>
        /// <param name="version">La versión SOAP del mensaje</param>
        /// <param name="fault">El objecto System.ServiceModel.Channels.Message que es retornado al cliente, o el servicio en caso duplex</param>
        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
            if (error is FaultException<ApplicationServiceError>)
            {
                MessageFault messageFault = ((FaultException<ApplicationServiceError>)error).CreateMessageFault();

                fault = Message.CreateMessage(version, messageFault, ((FaultException<ApplicationServiceError>)error).Action);
            }
            else
            {
                ApplicationServiceError defaultError = new ApplicationServiceError();

                if (error is ApplicationErrorException)
                {
                    defaultError.ErrorMessage = ((ApplicationErrorException)error).Message;
                }
                else
                {
                    defaultError.ErrorMessage = Messages.Message_DefaultErrorMessage;
                }

                FaultException<ApplicationServiceError> defaultFaultException = new FaultException<ApplicationServiceError>(defaultError);
                MessageFault defaultMessageFault = defaultFaultException.CreateMessageFault();

                fault = Message.CreateMessage(version, defaultMessageFault, defaultFaultException.Action);
            }
        }
    }
}
