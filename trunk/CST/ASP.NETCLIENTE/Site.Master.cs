using System;
using System.Globalization;
using System.Text;
using System.Threading;
using Infrastructure.CrossCutting.NetFramework.Util;


namespace ASP.NETCLIENTE
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es-CO");

        }

        protected override void OnInit(EventArgs e)
        {
            
            base.OnInit(e);
            RenderJavaScriptLinks();
        }

        private void RenderJavaScriptLinks()
        {
            var sb = new StringBuilder();
            var appRoot = UrlHelper.GetApplicationPath();

            var javaScripts = new[]
                                       {
                                           "Resources/Scripts/Site.js",
                                           "Resources/Scripts/jquery-1.8.2.min.js",
                                           "Resources/modules/chosen/chosen.jquery.js",                                           
                                           "Resources/modules/tableFilter/picnet.table.filter.min.js",    
                                       };

            foreach (var javaScript in javaScripts)
            {
                sb.AppendFormat("<script src=\"{0}\" type=\"text/javascript\"></script>\n\t", appRoot + javaScript);
            }
            JavaScripts.Text = sb.ToString();
        }
    }
}
