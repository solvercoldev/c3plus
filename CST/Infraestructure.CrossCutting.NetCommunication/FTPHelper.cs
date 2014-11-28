using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Infraestructure.CrossCutting.NetCommunication
{
    public class FTPHelper : IFtpHelper
    {
        #region Members

        private FtpWebRequest oFTPRequest;

        #endregion

        #region IFtpService Members

        #region Properties

        public string FTP_Site { get; set; }

        public string FTP_Port { get; set; }

        public string FTP_User { get; set; }

        public string FTP_Password { get; set; }

        public string DownloadFolderPath { get; set; }

        public bool DeleteBeforeDownload { get; set; }

        #endregion

        #region Methods

        public void Download(List<string> filesUri)
        {
            string sDateDirectory = DateTime.Now.ToString("yyyyMMdd");
            DownloadFolderPath = string.Format(@"{0}{1}", DownloadFolderPath, sDateDirectory);
            if (!Directory.Exists(DownloadFolderPath))
                Directory.CreateDirectory(DownloadFolderPath);
            try
            {
                foreach (string sFileName in filesUri)
                {
                    FileStream outputStream = new FileStream(Path.Combine(DownloadFolderPath, sFileName), FileMode.Create);

                    SetFTPData(sFileName);

                    oFTPRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                    FtpWebResponse response = (FtpWebResponse)oFTPRequest.GetResponse();
                    Stream ftpStream = response.GetResponseStream();
                    long cl = response.ContentLength;
                    int bufferSize = 2048;
                    int readCount;
                    byte[] buffer = new byte[2048];

                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                    while (readCount > 0)
                    {
                        outputStream.Write(buffer, 0, readCount);
                        readCount = ftpStream.Read(buffer, 0, bufferSize);
                    }

                    ftpStream.Close();
                    outputStream.Close();
                    response.Close();

                    //Luego de descargar el archivo se debe eliminar del ftp
                    if (DeleteBeforeDownload)
                    {
                        SetFTPData(sFileName);
                        oFTPRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                        response = (FtpWebResponse)oFTPRequest.GetResponse();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error en descarga de archivos de FTP, Error: {0}", ex.InnerException != null ? ex.InnerException.Message : ex.Message));
            }
        }

        public void SetFTPData(string file)
        {
            if (string.IsNullOrEmpty(file))
            {
                oFTPRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(string.Format("ftp://{0}", FTP_Site)));
            }
            else
            {
                oFTPRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(string.Format("ftp://{0}/{1}", FTP_Site, file)));
            }

            oFTPRequest.UseBinary = true;
            oFTPRequest.Credentials = new NetworkCredential(FTP_User, FTP_Password);
        }

        public List<string> GetFilesList()
        {
            List<string> oReturn = new List<string>();
            StringBuilder result = new StringBuilder();

            try
            {
                SetFTPData(string.Empty);

                oFTPRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse response = oFTPRequest.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string line = reader.ReadLine();
                while (line != null)
                {
                    if (line.Length > 2)
                    {
                        oReturn.Add(line);
                    }
                    line = reader.ReadLine();
                }

                reader.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error en lectura de directorio FTP, Error:", ex.InnerException != null ? ex.InnerException.Message : ex.Message));
            }
            return oReturn;
        }

        #endregion

        #endregion
    }
}
