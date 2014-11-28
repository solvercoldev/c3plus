using System.Collections.ObjectModel;
using System.ServiceModel;

using ServiceAgents.Proxies.AuthenticationService;
using System.Configuration;

namespace ServiceAgents
{
    /// <summary>
    /// Clase manejadora de servicios.
    /// </summary>
    public class ServiceManager
    {
        #region Members

        // Singleton Instance
        private static ServiceManager _instance;

        // Config Values
        private string _applicationServiceUri = ConfigurationManager.AppSettings.Get("AplicationServiceURI");

        // WCF Services
        private AuthenticationServiceClient _authenticationService;

        #endregion

        #region Properties

        /// <summary>
        /// Singleton del manejador de servicios
        /// </summary>
        public static ServiceManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ServiceManager();
                return _instance;
            }
        }

        /// <summary>
        /// Obtiene el servicio de autenticacion.
        /// </summary>
        public AuthenticationServiceClient AuthenticationService
        {
            get { return _authenticationService; }
        }

        #endregion

        #region Builders

        /// <summary>
        /// Inicializa una instancia default del manejador de servicios.
        /// </summary>
        public ServiceManager()
        {
            InitWCFServices();
        }

        #endregion

        #region Methods

        void InitWCFServices()
        {
            // Iniciando instancias
            _authenticationService = new AuthenticationServiceClient();
        }

        #endregion
    }
}
