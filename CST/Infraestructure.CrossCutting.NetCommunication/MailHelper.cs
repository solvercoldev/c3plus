using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.IO;
using System.Net;

namespace Infraestructure.CrossCutting.NetCommunication
{
    public class MailHelper : IMailHelper
    {
        #region Members

        // Variables de Configuracion de Correo
        private SmtpClient _Smtp;
        private MailMessage _MailMessage;

        #endregion

        #region Builders

        public MailHelper()
        {

        }

        #endregion

        #region IMailHelper Members

        #region Properties

        public bool SMTP_EnableSSL { get; set; }

        public int SMTP_Port { get; set; }

        public string SMTP_Host { get; set; }

        public string SMTP_User { get; set; }

        public string SMTP_Password { get; set; }

        public string SMTP_From { get; set; }

        public string SMTP_Subject { get; set; }

        public string SMTP_Body { get; set; }

        public string[] SMTP_To { get; set; }

        public string[] SMTP_CC { get; set; }

        public string[] SMTP_BCC { get; set; }

        public string[] AttachementsFileList { get; set; }

        #endregion

        #region Methods

        public void SendMail()
        {
            // Inicializando Objetos
            _Smtp = new SmtpClient(SMTP_Host, SMTP_Port);
            _Smtp.Credentials = new NetworkCredential(SMTP_User, SMTP_Password);
            _Smtp.EnableSsl = SMTP_EnableSSL;
            _MailMessage = new MailMessage();

            // Creando Correo
            _MailMessage.From = new MailAddress(SMTP_From);
            _MailMessage.Subject = SMTP_Subject;
            _MailMessage.Body = SMTP_Body;
            _MailMessage.IsBodyHtml = true;
            // Adicionando Destinatarios
            foreach (string sTO in SMTP_To)
            {
                if (!string.IsNullOrEmpty(sTO))
                    _MailMessage.To.Add(sTO);
            }
            // Adicionando Copia
            if (SMTP_CC != null)
            {
                foreach (string sCC in SMTP_CC)
                {
                    if (!string.IsNullOrEmpty(sCC))
                        _MailMessage.CC.Add(sCC);
                }
            }            
            // Adicionando Copia Oculta
            if (SMTP_BCC != null)
            {
                foreach (string sBCC in SMTP_BCC)
                {
                    if (!string.IsNullOrEmpty(sBCC))
                        _MailMessage.Bcc.Add(sBCC);
                }
            }            
            // Adicionando Archivos Adjuntos
            if (AttachementsFileList != null)
            {
                foreach (string sFile in AttachementsFileList)
                {
                    //FileInfo oFile = new FileInfo(sFile);
                    _MailMessage.Attachments.Add(new Attachment(sFile));
                }
            }            

            //Sending Mail
            try
            {
                _Smtp.Send(_MailMessage);
            }
            catch (SmtpException ex)
            {
                throw new Exception(string.Format("Cls:MailHelper, Mtd: SendMail, Error en enviar mail, Error:{0}", ex.InnerException == null ? ex.Message : ex.InnerException.Message));                
            }
        }

        #endregion

        #endregion        
    }
}
