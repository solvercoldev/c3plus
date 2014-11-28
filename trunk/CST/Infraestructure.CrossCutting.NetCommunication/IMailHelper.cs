
namespace Infraestructure.CrossCutting.NetCommunication
{
    public interface IMailHelper
    {
        bool SMTP_EnableSSL { get; set; }
        int SMTP_Port { get; set; }

        string SMTP_Host { get; set; }
        string SMTP_User { get; set; }
        string SMTP_Password { get; set; }
        string SMTP_From { get; set; }
        string SMTP_Subject { get; set; }
        string SMTP_Body { get; set; }

        string[] SMTP_To { get; set; }
        string[] SMTP_CC { get; set; }
        string[] SMTP_BCC { get; set; }

        string[] AttachementsFileList { get; set; }

        void SendMail();
    }
}
