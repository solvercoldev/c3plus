using System.Collections.Generic;
using System.IO;

namespace Infrastructure.CrossCutting.NetFramework.Services.Email
{


    public interface IEmailService
    {
        /// <summary>
        /// The physical template directory. This can be set from code or via a configuration parameter.
        /// </summary>
        string TemplateDir
        {
            set;
        }

        /// <summary>
        /// The two-letter ISO language code (for example, 'en') that is used to load language-specific templates.
        /// When left null, no language-specific template is assumed.
        /// </summary>
        string Language
        {
            set;
        }

        /// <summary>
        /// Send an email based on a template. The template contains the subject and the body with optional
        /// placeholders for dynamic content.
        /// </summary>
        /// <param name="from">The from address</param>
        /// <param name="to">The to address</param>
        /// <param name="templateName">The name of the template</param>
        /// <param name="subjectParams">Dynamic subject parameters</param>
        /// <param name="bodyParams">Dynamic body parameters</param>
        void ProcessEmail(string from, string to, string templateName, Dictionary<string, string> subjectParams, Dictionary<string, string> bodyParams);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="contentTemplate"></param>
        /// <param name="subjectParams"></param>
        /// <param name="bodyParams"></param>
        /// <param name="cc"></param>
        /// <param name="attached"></param>
        void ProcessEmail(string from, string to, string contentTemplate, Dictionary<string, string> subjectParams,
                          Dictionary<string, string> bodyParams, string[] cc, Stream[] attached);

        /// <summary>
        /// Retorna un vector de dos posiciones con el Subject[0] y Body[1] mapeados con los valores
        /// entregados al metodo en los respectivos diccionarios.
        /// </summary>
        /// <param name="contentTemplate"></param>
        /// <param name="subjectParams"></param>
        /// <param name="bodyParams"></param>
        /// <returns></returns>
        string[] MergeTemplate(string contentTemplate, Dictionary<string, string> subjectParams,
                               Dictionary<string, string> bodyParams);
    }    
}
