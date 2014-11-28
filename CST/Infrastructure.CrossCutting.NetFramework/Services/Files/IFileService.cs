using System.IO;

namespace Infrastructure.CrossCutting.NetFramework.Services.Files
{
    public interface IFileService
    {

        /// <summary>
        /// Lee el contenido del archivo dentro del stream.
        /// </summary>
        /// <remarks>
        /// NOTE: El llamador es el responsable de cerrar el stream.
        /// </remarks>
        /// <param name="filePath">Ruta de acceso físico donde se encuentra el archivo.</param>
        /// <returns></returns>
        Stream ReadFile(string filePath);

        /// <summary>
        /// Escribe el contenido dado en el stream, en un archivo físico.
        /// </summary>
        /// <remarks>
        /// NOTE: El llamador es el responsable de cerrar el stream.
        /// </remarks>
        /// <param name="filePath">Ruta de acceso físico donde se encuentra el archivo</param>
        /// <param name="fileContents"></param>
        void WriteFile(string filePath, Stream fileContents);

        /// <summary>
        /// Elimina un unico archivo.
        /// </summary>
        /// <param name="filePath">Ruta de acceso físico donde se encuentra el archivo</param>
        void DeleteFile(string filePath);

        /// <summary>
        /// comprueba si el directorio tiene permisos de escritura para el usuario actual..
        /// </summary>
        /// <param name="physicalDirectory"></param>
        /// <returns></returns>
        bool CheckIfDirectoryIsWritable(string physicalDirectory);
    }
}
