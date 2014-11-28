using System;
using System.Linq;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace DistributedServices.Core.ErrorHandlers
{
    /// <summary>
    /// Comportamiento de servicio para añadir DefaultErrorHandler a todos
    /// los despachadores in WCF
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ApplicationErrorHandlerAttribute
        : Attribute, IServiceBehavior
    {
        /// <summary>
        /// Provee la habilidad para pasar datos personalizados a elementos enlazados para
        /// soportar la implementación del contrato
        /// </summary>
        /// <param name="serviceDescription">La descripción del servicio</param>
        /// <param name="serviceHostBase">El hosteador del servicio</param>
        /// <param name="endpoints">Los puntos finales del servicio</param>
        /// <param name="bindingParameters">Los parámetros enlazados</param>
        public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {

        }
        /// <summary>
        /// Provee la habilidad para cambiar los valores de la propiedad del runtime o
        /// insertar objetos de extensión personalizados como manejadores de errores,
        /// mensajes o interceptores de parámetros, extensiones de seguridad, y otros
        /// objetos extensión personalizados
        /// </summary>
        /// <param name="serviceDescription">La descripción del servicio</param>
        /// <param name="serviceHostBase">El host que esta actualmente siendo construido</param>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            if (serviceHostBase != null && serviceHostBase.ChannelDispatchers.Any())
            {
                foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
                    dispatcher.ErrorHandlers.Add(new ApplicationErrorHandler());
            }
        }
        /// <summary>
        /// Provee la habilidad para onspeccionar el servicio hosteador y la descripción del servicio
        /// para confirmar que el servicio pueda correr exitosamente
        /// </summary>
        /// <param name="serviceDescription">La descripción del servicio</param>
        /// <param name="serviceHostBase">El hosteador del servicio que esta actualmente siendo construido</param>
        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {

        }
    }
}
