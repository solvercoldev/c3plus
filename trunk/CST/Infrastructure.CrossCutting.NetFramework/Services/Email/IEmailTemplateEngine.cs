using System.Collections.Generic;

namespace Infrastructure.CrossCutting.NetFramework.Services.Email
{
    public interface IEmailTemplateEngine
    {
        /// <summary>
        /// Loads an email template and merges the optional parameters into the template.
        /// </summary>
        /// <param name="template">Contenido del cuerpo del mensaje</param>
        /// <param name="subjectParams"></param>
        /// <param name="bodyParams"></param>
        /// <returns>The merged email subject and body (position 0 is the subject, position 1 is the body)</returns>
        
        string[] ProcessTemplate(string template, Dictionary<string, string> subjectParams, Dictionary<string, string> bodyParams);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="templatePath"></param>
        /// <param name="subjectParams"></param>
        /// <param name="bodyParams"></param>
        /// <returns></returns>
        string[] ProcessTemplateFromUrl(string templatePath, Dictionary<string, string> subjectParams,Dictionary<string, string> bodyParams);
    }
}