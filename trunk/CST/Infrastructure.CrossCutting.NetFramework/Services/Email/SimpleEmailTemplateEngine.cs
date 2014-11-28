using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using log4net;

namespace Infrastructure.CrossCutting.NetFramework.Services.Email
{
    /// <summary>
    /// Implements IEmailTemplateEngine. The template is a simple text file with tags that indicate the subject part
    /// and the body part.
    /// </summary>
    /// <example>
    /// Example template:
    /// [subject]
    /// This is an example subject with dynamic placeholder $placeholder
    /// [/subject]
    /// [body]
    /// This is an example email body text with another placeholder: $anotherPlaceholder.
    /// [/body]
    /// </example>
    public class SimpleEmailTemplateEngine : IEmailTemplateEngine
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SimpleEmailTemplateEngine));

        #region IEmailTemplateEngine Members

        public string[] ProcessTemplate(string template, Dictionary<string, string> subjectParams, Dictionary<string, string> bodyParams)
        {

            if (string.IsNullOrEmpty(template)) return null;
            var emailTemplateContent = template;
           
            try
            {
                var subjectRegex = new Regex(@"\[subject\]\r\n(.*)\r\n\[\/subject\]"
               , RegexOptions.Compiled | RegexOptions.Singleline);
                var bodyRegex = new Regex(@"\[body\]\r\n(.*)\r\n\[/body\]"
                    , RegexOptions.Compiled | RegexOptions.Singleline);

                var subject = subjectRegex.Match(emailTemplateContent).Groups[1].Value;
                var body = bodyRegex.Match(emailTemplateContent).Groups[1].Value;

                subject = ReplacePlaceholdersWithValues(subjectParams, subject);
                body = ReplacePlaceholdersWithValues(bodyParams, body);

                return new[] { subject, body };
            }
            catch (Exception ex)
            {
                Log.Error("Template Email:", ex);
                throw;
            }
        }

        public string[] ProcessTemplateFromUrl(string templatePath, Dictionary<string, string> subjectParams, Dictionary<string, string> bodyParams)
        {

            if (string.IsNullOrEmpty(templatePath)) return null;

            string emailTemplateContent;
            using (var sr = new StreamReader(templatePath))
            {
                emailTemplateContent = sr.ReadToEnd();
            }
            try
            {
                var subjectRegex = new Regex(@"\[subject\]\r\n(.*)\r\n\[\/subject\]"
               , RegexOptions.Compiled | RegexOptions.Singleline);
                var bodyRegex = new Regex(@"\[body\]\r\n(.*)\r\n\[/body\]"
                    , RegexOptions.Compiled | RegexOptions.Singleline);

                var subject = subjectRegex.Match(emailTemplateContent).Groups[1].Value;
                var body = bodyRegex.Match(emailTemplateContent).Groups[1].Value;

                subject = ReplacePlaceholdersWithValues(subjectParams, subject);
                body = ReplacePlaceholdersWithValues(bodyParams, body);

                return new[] { subject, body };
            }
            catch (Exception ex)
            {
                Log.Error("Template Email:", ex);
                throw;
            }
        }

        private static string ReplacePlaceholdersWithValues(Dictionary<string, string> parameters, string textWithPlaceholders)
        {
            return parameters.Aggregate(textWithPlaceholders, (current, param) => current.Replace(param.Key, param.Value));
        }

        #endregion
    }
}