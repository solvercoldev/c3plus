using System;
using System.Collections.Generic;
using System.IO;
using log4net;

namespace Infrastructure.CrossCutting.NetFramework.Services.Email
{
    public class DefaultEmailService : IEmailService
    {
        private static ILog log = LogManager.GetLogger(typeof(DefaultEmailService));
        private const string DefaultExtension = ".txt";
        private string _templateDir;
        private string _language;
        private readonly IEmailSender _emailSender;
        private readonly IEmailTemplateEngine _templateEngine;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="emailSender"></param>
        /// <param name="templateEngine"></param>
        public DefaultEmailService(IEmailSender emailSender, IEmailTemplateEngine templateEngine)
        {
            _emailSender = emailSender;
            _templateEngine = templateEngine;
        }

        #region IEmailService Members

        /// <summary>
        /// 
        /// </summary>
        public string TemplateDir
        {
            set { _templateDir = value; }
        }

        /// <summary>
        /// If a language is specified, a language extension is added after template name (for example, MyTemplate.en.txt).
        /// </summary>
        public string Language
        {
            set { _language = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="templateName"></param>
        /// <param name="subjectParams"></param>
        /// <param name="bodyParams"></param>
        public void ProcessEmail(string from, string to, string templateName, Dictionary<string, string> subjectParams, Dictionary<string, string> bodyParams)
        {
            var templatePath = DetermineTemplatePath(templateName);
            try
            {
                var subjectAndBody = _templateEngine.ProcessTemplate(templatePath, subjectParams, bodyParams);
                try
                {
                    _emailSender.Send(from, to, subjectAndBody[0], subjectAndBody[1]);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to send email", ex);
                }
            }
            catch (Exception ex)
            {
                log.Error("Unable to process email message", ex);
                throw;
            }
        }

        public void ProcessEmail(string from, string to, string contentTemplate, Dictionary<string, string> subjectParams, Dictionary<string, string> bodyParams, string[] cc, Stream[] attached)
        {

            try
            {
                var subjectAndBody = _templateEngine.ProcessTemplate(contentTemplate, subjectParams, bodyParams);
                try
                {
                    _emailSender.Send(from, to, subjectAndBody[0], subjectAndBody[1], cc, null, attached);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to send email", ex);
                }
            }
            catch (Exception ex)
            {
                log.Error("Unable to process email message", ex);
                throw;
            }
        }

        /// <summary>
        /// Retorna la plantilla combinadas con los valores pasados por parámetro
        /// </summary>
        /// <returns>[0] = Subjet, [1]=Body</returns>
        public string[] MergeTemplate(string contentTemplate, Dictionary<string, string> subjectParams, Dictionary<string, string> bodyParams)
        {
            return _templateEngine.ProcessTemplate(contentTemplate, subjectParams, bodyParams);
        }

        #endregion

        /// <summary>
        /// By default, the physical template file name consists of the template name with a .txt extension.
        /// If a language is specified, a language extension is added after template name (for example, MyTemplate.en.txt).
        /// If a language is specified, but no template is found, the method tries to find a template without the 
        /// language extension.
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        protected virtual string DetermineTemplatePath(string templateName)
        {
            var fileName = templateName + DefaultExtension;
            if (_language != null)
            {
                var fileNameWithLanguage = templateName + "." + _language.ToLower() + DefaultExtension;
                var filePathWithLanguage = Path.Combine(_templateDir, fileNameWithLanguage);
                // Check if file exists. If yes, return the filePathWithLanguage, otherwise continue.
                if (File.Exists(filePathWithLanguage))
                {
                    return filePathWithLanguage;
                }
            }
            var filePath = Path.Combine(_templateDir, fileName);
            if (File.Exists(filePath))
            {
                return filePath;
            }
            throw new FileNotFoundException("Unable to find the email template: " + templateName);
        }
    }
}