using System;
using Infrastructure.CrossCutting.IoC;
using Microsoft.Practices.Unity;

namespace ASP.NETCLIENTE.Utils
{
    public class ContainerAccessorUtil
    {

        /// <summary>
        /// Obteniendo el contenedor para la aplicacion web
        /// </summary>
        /// <returns></returns>
        public static IUnityContainer GetContainer()
        {
            var containerAccessor = IoC.Container;

            if (containerAccessor == null)
            {
                throw new Exception("Debe extender el HTTPApplication en el proyecto web " +
                    "e implementar el IContainerAccessor para exponer correctamente la instancia del contenedor");
            }

            return containerAccessor;
        }
    }
}