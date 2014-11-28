using System;

namespace Application.Core
{
    /// <summary>
    /// La excepción personalizada para errores en la capa de aplicación
    /// </summary>
    public class ApplicationErrorException
        : Exception
    {
        /// <summary>
        /// Crea una instancia de ApplicationErrorException 
        /// </summary>
        /// <param name="errorMessage">El mensaje de error</param>
        public ApplicationErrorException(string errorMessage)
            : base(errorMessage)
        {

        }
    }
}
