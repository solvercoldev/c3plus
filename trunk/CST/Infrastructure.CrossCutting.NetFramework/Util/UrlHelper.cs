using System.Web;

namespace Infrastructure.CrossCutting.NetFramework.Util
{
    public class UrlHelper
    {

        /// <summary>
        /// GetApplicationPath returns the base application path and ensures that it allways ends with a "/".
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationPath()
        {
            return UrlUtil.GetApplicationPath();
        }

        /// <summary>
        /// Get the (lowercase) url of the site without any trailing slashes.
        /// </summary>
        /// <returns></returns>
        public static string GetSiteUrl()
        {
            return UrlUtil.GetSiteUrl();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathInfo"></param>
        /// <returns></returns>
        public static string[] GetParamsFromPathInfo(string pathInfo)
        {
            return UrlUtil.GetParamsFromPathInfo(pathInfo);
        }

        /// <summary>
        /// Returns a formatted url for a rss feed for a given section 
        /// (http://{hostname}/{ApplicationPath}/{Section.Id}/rss.aspx).
        /// </summary>
        /// <returns></returns>
        public static string GetRssUrlFromSection(string idSection)
        {
            return GetHostUrl() + GetApplicationPath() + idSection + "/feed.aspx";
        }

        /// <summary>
        /// Returns a formatted url for a given section (/{ApplicationPath}/{Section.Id}/section.aspx).
        /// </summary>
        /// <param name="idSection"></param>
        /// <returns></returns>
        public static string GetUrlFromSection(string idSection)
        {
            return UrlUtil.GetUrlFromSection(idSection);
        }


        /// <summary>
        /// Returns a formatted url for a given section (http://{hostname}/{ApplicationPath}/{Section.Id}/section.aspx).
        /// </summary>
        /// <param name="idSection"></param>
        /// <returns></returns>
        public static string GetFullUrlFromSection(string idSection)
        {
            return UrlUtil.GetFullUrlFromSection(idSection);
        }


        public static string GetUrlFromNode(string idNode)
        {
            return UrlUtil.GetUrlFromNode(idNode);
        }

        /// <summary>
        /// Get the full url of a Node with the host url resolved via the Site property.
        /// </summary>
        /// <param name="siteHomeUrl"></param>
        /// <param name="externalLink"></param>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public static string GetFullUrlFromNodeViaSite(string siteHomeUrl, bool externalLink, string nodeId)
        {
            return UrlUtil.GetFullUrlFromNodeViaSite(externalLink, siteHomeUrl, nodeId);
        }

        public static string GetUrlPreViewForNotificationWindow()
        {
            return UrlUtil.GetUrlPreViewForNotificationWindow();
        }

        public static string GetUrlPreViewDocumentforEmail()
        {
            return UrlUtil.GetUrlPreViewDocumentforEmail();
        }

        public static string GetUrlPreViewComment(string idComentario)
        {
            return UrlUtil.GetUrlPreViewComment(idComentario);
        }

        public static string GetUrlPreViewAlternativaSolucion(string idAlternativa)
        {
            return UrlUtil.GetUrlPreViewAlternativaSolucion(idAlternativa);
        }

        public static string GetUrlPreViewActividad(string idActividad)
        {
              return UrlUtil.GetUrlPreViewActividad(idActividad);
        }

        #region Solicitudes

        public static string GetUrlPreViewDocumentSolicitudforEmail()
        {
            return UrlUtil.GetUrlPreViewDocumentSolicitudforEmail();
        }

        #endregion


        private static string GetHostUrl()
        {
            string securePort = HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
            string protocol = securePort == null || securePort == "0" ? "http" : "https";
            string serverPort = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            string port = serverPort == "80" ? string.Empty : ":" + serverPort;
            string serverName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
            return string.Format("{0}://{1}{2}", protocol, serverName, port);
        }
    }
}