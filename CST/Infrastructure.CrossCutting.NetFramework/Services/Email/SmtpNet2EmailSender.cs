using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Infrastructure.CrossCutting.NetFramework.Services.Email
{
    /// <summary>
    /// Implements IEmailSender using the System.Net.Email classes from the .NET 2.0 framework.
    /// </summary>
    public class SmtpNet2EmailSender : IEmailSender
    {
        private string _host;
        private int _port;
        private string _smtpUsername;
        private string _smtpPassword;
        private Encoding _encoding;

        /// <summary>
        /// SMTP port (default 25).
        /// </summary>
        public int Port
        {
            set { _port = value; }
        }

        /// <summary>
        /// SMTP Username
        /// </summary>
        public string SmtpUsername
        {
            set { _smtpUsername = value; }
        }

        /// <summary>
        /// SMTP Password
        /// </summary>
        public string SmtpPassword
        {
            set { _smtpPassword = value; }
        }

        /// <summary>
        /// Email body encoding
        /// </summary>
        public string EmailEncoding
        {
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _encoding = Encoding.GetEncoding(value);
                }
            }
        }

       
        public SmtpNet2EmailSender()
        {
            _host = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("host")) ? string.Empty : ConfigurationManager.AppSettings.Get("host");
            _smtpPassword = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("smtpPassword")) ? string.Empty : ConfigurationManager.AppSettings.Get("smtpPassword");
            _smtpUsername = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("smtpUsername")) ? string.Empty : ConfigurationManager.AppSettings.Get("smtpUsername");
            _port = ConfigurationManager.AppSettings.Get("port") == "" ? 25 : Convert.ToInt32(ConfigurationManager.AppSettings.Get("port"));
            _encoding = Encoding.Default;
        }

        #region IEmailSender Members

        public void Send(string from, string to, string subject, string body)
        {
            Send(from, to, subject, body, null, null, null);
        }

        public void Send(string from, string to, string subject, string body, string[] cc, string[] bcc, Stream[] documentoAdjunto)
        {
            // Create mail message
            var message = new MailMessage(from, to, subject, body)
                              {
                                  BodyEncoding = _encoding,
                                  IsBodyHtml = true
                              };

            if (cc != null && cc.Length > 0)
            {
                foreach (string ccAddress in cc)
                {
                    message.CC.Add(new MailAddress(ccAddress));
                }
            }
            if (bcc != null && bcc.Length > 0)
            {
                foreach (string bccAddress in bcc)
                {
                    message.Bcc.Add(new MailAddress(bccAddress));
                }
            }

            if (documentoAdjunto != null && documentoAdjunto.Length > 0)
            {
                foreach (var stream in documentoAdjunto)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    var att = new Attachment(stream, "Click para descargar el documento adjunto...");
                    message.Attachments.Add(att);
                }
            }

            // Send email
            var client = new SmtpClient(_host, _port);
            if (!String.IsNullOrEmpty(_smtpUsername) && !String.IsNullOrEmpty(_smtpPassword))
            {
                client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
            }
            client.Send(message);
        }

      
        #endregion
    }
}