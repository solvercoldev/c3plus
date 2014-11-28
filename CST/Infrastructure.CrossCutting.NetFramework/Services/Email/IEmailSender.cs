using System.IO;

namespace Infrastructure.CrossCutting.NetFramework.Services.Email
{
    public interface IEmailSender
    {
        /// <summary>
        /// Enviar un correo electrónico con los parámetros dados (que hablan por sí mismos).
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        void Send(string from, string to, string subject, string body);

        /// <summary>
        /// Enviar un correo electrónico con los parámetros dados (que hablan por sí mismos).
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="cc"></param>
        /// <param name="bcc"></param>
        /// <param name="documentoAdjunto"></param>
        void Send(string from, string to, string subject, string body, string[] cc, string[] bcc, Stream[] documentoAdjunto);

       
    }
}