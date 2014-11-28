using System.Runtime.Serialization;

namespace DistributedServices.Core.ErrorHandlers
{
    /// <summary>
    /// ServiceError predeterminado
    /// </summary>
    [DataContract(Name = "ServiceError", Namespace = "DistributedServices.Core")]
    public class ApplicationServiceError
    {
        /// <summary>
        /// Mensaje de error que se envía al servicio del cliente
        /// </summary>
        [DataMember(Name = "ErrorMessage")]
        public string ErrorMessage { get; set; }
    }
}
