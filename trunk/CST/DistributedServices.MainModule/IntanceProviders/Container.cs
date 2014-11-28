using Microsoft.Practices.Unity;
using Infrastructure.Data.MainModule.UnitOfWork;
using Domain.MainModule.Contracts;
using Infrastructure.CrossCutting.Logging;
using Infrastructure.CrossCutting.NetFramework.Logging;
using Infraestructure.Data.Core;

namespace DistributedServices.MainModule.IntanceProviders
{
    public static class Container
    {
        #region Properties

        static IUnityContainer _currentContainer;

        /// <summary>
        /// Get the current configured container
        /// </summary>
        /// <returns>Configured container</returns>
        public static IUnityContainer Current
        {
            get
            {
                return _currentContainer;
            }
        }

        #endregion

        #region Constructor

        static Container()
        {
            ConfigureContainer();

            ConfigureFactories();
        }

        #endregion

        #region Methods

        static void ConfigureContainer()
        {
            _currentContainer = new UnityContainer();

            _currentContainer.RegisterType<IMainModuleUnitOfWork, MainModuleContext>(new PerResolveLifetimeManager(), new InjectionConstructor());

            #region Repositorios

            //_currentContainer.RegisterType<ITBL_Maestra_OpcionesMenuRepository, TBL_Maestra_OpcionesMenuRepository>();
            //_currentContainer.RegisterType<ITBL_Maestra_RolesRepository, TBL_Maestra_RolesRepository>();
            //_currentContainer.RegisterType<ITBL_Maestra_UsuariosRepository, TBL_Maestra_UsuariosRepository>();

            #endregion

            #region Crosscutting

            _currentContainer.RegisterType<ITraceManager, TraceManager>(new TransientLifetimeManager());
            //_currentContainer.RegisterType<IAutentication, DefaultAuthentication>(new TransientLifetimeManager());
            _currentContainer.RegisterType<ISqlHelper, SqlHelper>(new TransientLifetimeManager());

            #endregion

            // Servicios WCF
            _currentContainer.RegisterType<IAuthenticationService, AuthenticationService>();
        }

        static void ConfigureFactories()
        {
        }

        #endregion
    }
}