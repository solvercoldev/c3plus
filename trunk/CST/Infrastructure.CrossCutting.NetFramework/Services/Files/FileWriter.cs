using System;
using System.Collections;
using System.IO;

namespace Infrastructure.CrossCutting.NetFramework.Services.Files
{
    public class FileWriter
    {
        private readonly string _tempDir = Environment.GetEnvironmentVariable("TEMP");

        private readonly IList _createdFiles = new ArrayList();
        private readonly IList _deletedFiles = new ArrayList();

        /// <summary>
        /// Contructor.
        /// </summary>
        public FileWriter(string tempDir)
        {
            if (!String.IsNullOrEmpty(tempDir))
            {
                _tempDir = tempDir;
            }
        }

        /// <summary>
        /// Create a file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="inputStream"></param>
        public void CreateFromStream(string filePath, Stream inputStream)
        {
            if (File.Exists(filePath))
            {
                throw new ArgumentException("El archivo ya existe.", filePath);
            }
            var directoryName = Path.GetDirectoryName(filePath);
            if (directoryName != null)
                if (!Directory.Exists(directoryName))
                {
                    throw new DirectoryNotFoundException(String.Format("El directorio {0} no existe.", directoryName));
                }

            var tempFilePath = Path.Combine(_tempDir, Path.GetFileName(filePath));

            var fs = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write);
            StreamUtil.Copy(inputStream, fs);
            fs.Flush();
            fs.Close();

            _createdFiles.Add(new string[2] { filePath, tempFilePath });
        }

        /// <summary>
        /// Mark a file for deletion by adding it to the list of file that are going to be deleted.
        /// </summary>
        /// <param name="filePath"></param>
        public void DeleteFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("El archivo no se encontro, por lo que no pudo ser eliminado.", filePath);
            }
            _deletedFiles.Add(filePath);
        }

        #region IResource Members

        public void Start()
        {

        }

        public void Rollback()
        {
            // Elimina los archivos termporales
            foreach (string[] newFileLocations in _createdFiles)
            {
                // index 0 localizacion permanente, index 1 = localizacion temporal
                File.Delete(newFileLocations[1]);
            }
        }

        public void Commit()
        {
            // Mueve los archivos de la localizacion temporal a la permanente.
            foreach (string[] newFileLocations in _createdFiles)
            {
                // index 0 localizacion permanente, index 1 = localizacion temporal
                File.Move(newFileLocations[1], newFileLocations[0]);
            }

            // Borrar eliminaciones programadas
            foreach (string fileToBeDeleted in this._deletedFiles)
            {
                File.Delete(fileToBeDeleted);
            }
        }
        #endregion

    }
}
