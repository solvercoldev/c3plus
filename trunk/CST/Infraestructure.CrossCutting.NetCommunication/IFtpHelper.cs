using System.Collections.Generic;

namespace Infraestructure.CrossCutting.NetCommunication
{
    public interface IFtpHelper
    {
        // Propiedades de FTP
        string FTP_Site { get; set; }
        string FTP_Port { get; set; }
        string FTP_User { get; set; }
        string FTP_Password { get; set; }
        string DownloadFolderPath { get; set; }
        bool DeleteBeforeDownload { get; set; }

        // Metodos de FTP        
        void Download(List<string> filesUri);
        void SetFTPData(string file);
        List<string> GetFilesList();
    }
}
