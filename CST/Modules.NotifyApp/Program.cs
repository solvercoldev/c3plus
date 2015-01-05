using Infrastructure.CrossCutting.IoC;
using log4net.Config;
using Microsoft.Practices.Unity;
using Modules.Loader;
using System;

namespace Modules.NotifyApp
{
    class Program
    {
        /// <summary>
        /// Obteniendo el contenedor
        /// </summary>
        private static IUnityContainer Container
        {
            get { return IoC.Container; }
        }

        static void Main(string[] args)
        {
            Console.WriteLine(string.Format("Inicio de tarea de notificacion."));
            InitApp();

            NotifyManager.Instance.NotifyPendingTask();

            Console.WriteLine(string.Format("Fin de tarea de notificacion."));
        }

        static void InitApp()
        {
            Console.WriteLine(string.Format("Inicializando Repositorios."));
            ////Inicializando log 4Net.
            Console.WriteLine(string.Format("Init Log4Net."));
            XmlConfigurator.Configure();
            Console.WriteLine(string.Format("Fin Log4Net."));
            Console.WriteLine(string.Format("Init Container."));
            IoCFactory.InitializeContainer();
            Console.WriteLine(string.Format("Fin Container."));
            Console.WriteLine(string.Format("Init Modules."));
            IoCFactory.RegisterModuleLoader();
            Console.WriteLine(string.Format("Fin Modules."));

            Console.WriteLine(string.Format("Init ModuleContainer."));
            // Load module types into the container.
            var loader = Container.Resolve<ModuleLoader>();
            loader.LocalRegisterActivatedModules();
            Console.WriteLine(string.Format("Fin ModuleContainer."));

            Console.WriteLine(string.Format("Fin Cargue de Repositorios."));
        }
    }
}
