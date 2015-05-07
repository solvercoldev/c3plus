using Infrastructure.CrossCutting.IoC;
using log4net.Config;
using Microsoft.Practices.Unity;
using System;
using Modules.Loader;

namespace LoadAttachmentFiles
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
            Console.WriteLine(string.Format("Inicio de tarea de importación de anexos."));
            InitApp();

            LoadProcess.Instance.ProcessDirectory();

            Console.WriteLine(string.Format("Fin de tarea de importación de anexos."));
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
