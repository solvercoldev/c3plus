using System;
using System.IO;
using log4net;

namespace Infrastructure.CrossCutting.NetFramework.Services.Files
{
    public class TransactionalFileService : IFileService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(TransactionalFileService));
        

        /// <summary>
        /// Physical path for temporary file storage
        /// </summary>
        public string TempDir { private get; set; }


        public Stream ReadFile(string filePath)
        {
            try
            {
                var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return fs;
            }
            catch (FileNotFoundException ex)
            {
                Log.Error("Archivo no encontrado.", ex);
                throw;
            }
            catch (Exception ex)
            {
                Log.Error("Error inesperado durante la lectura del archivo.", ex);
                throw;
            }
        }

       
        public void WriteFile(string filePath, Stream fileContents)
        {

            try
            {

                var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                StreamUtil.Copy(fileContents, fs);
                fs.Flush();
                fs.Close();
            }
            catch (Exception)
            {
                throw;
            }
               
           
        }

        public void DeleteFile(string filePath)
        {
            try
            {
                File.Delete(filePath);
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        public bool CheckIfDirectoryIsWritable(string physicalDirectory)
        {

            var fileName = Path.Combine(physicalDirectory, "dummy.txt");

            try
            {
                using (var sw = new StreamWriter(fileName))
                {
                    sw.WriteLine("DUMMY");
                    sw.Flush();
                }
                File.Delete(fileName);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                Log.WarnFormat("La comprobacion de acceso a directorio fisico {0} no fue posible por falta de permisos.", physicalDirectory);
                return false;
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Un error inesperado a ocurrido mientras se comprobaba el acceso al directorio {0}.", physicalDirectory), ex);
                throw;
            }
        }
    }
}
