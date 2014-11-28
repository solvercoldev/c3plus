#region Usings
using System;
using Microsoft.Practices.Unity;

#endregion

namespace Infrastructure.CrossCutting.IoC
{
    public static class IoC
    {

        #region Members

        private static IUnityContainer _unityContainer;

        #endregion

        public static IUnityContainer Container
        {
            get
            {
                if (_unityContainer == null)
                {
                    throw new InvalidOperationException("The container has not been initialized!");
                }
                return _unityContainer;
            }
        }

        /// <summary>
        /// Indicates if the container is initialized properly.
        /// </summary>
        public static bool IsInitialized
        {
            get { return _unityContainer != null; }
        }

        /// <summary>
        /// Initialize the IoC wrapper
        /// </summary>
        /// <param name="unitContainer"></param>
        public static void Initialize(IUnityContainer unitContainer)
        {
            _unityContainer = unitContainer;
        }

        #region IServiceFactory Members

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static TService Resolve<TService>()
        {
            
            return Container.Resolve<TService>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object Resolve(Type type)
        {
            return Container.Resolve(type, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public static void RegisterType(Type type)
        {
            //var container = ContainersDictionary["RootContext"];

            if (Container != null)
                Container.RegisterType(type, new TransientLifetimeManager());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="servicetype"></param>
        /// <param name="classType"></param>
        public static void RegisterType(Type servicetype, Type classType)
        {
            if (Container != null)
                Container.RegisterType(servicetype, classType, new TransientLifetimeManager());

        }

        public static bool HasComponent(Type servicetype)
        {
            return (Container != null && Container.IsRegistered(servicetype));

        }

        public static object GetInstanceFromServiceName(Type sn)
        {
            object instance = null;
            foreach (var item in Container.Registrations)
            {
                instance = item.GetType();
            }
            return instance;
        }
        #endregion

    }
}