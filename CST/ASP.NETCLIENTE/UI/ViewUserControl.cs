using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using Application.Core;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.IoC;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace ASP.NETCLIENTE.UI
{
    public class ViewUserControl<TPresenter, TView> : BaseUserControl
        where TPresenter : Presenter<TView>
        where TView : class
    {

        private Literal TitulosVentana
        {
            get { return Page.Master != null ? Page.Master.FindControl("PageTitleLabel") as Literal : null; }
        }

        protected TPresenter Presenter { get; private set; }

        protected ViewUserControl()
        {
            var globalizacion = ConfigurationManager.AppSettings.Get("Globalizacion");
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(string.IsNullOrEmpty(globalizacion) ? "es-CO" : globalizacion);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(string.IsNullOrEmpty(globalizacion) ? "es-CO" : globalizacion);

            Presenter = IoC.Resolve<TPresenter>();
            Presenter.View = this as TView;
            Presenter.SubscribeViewToEvents();
            Presenter.LogProcesamientoEvent += PresenterLogProcesamientoEvent;
        }

        void PresenterLogProcesamientoEvent(object sender, LogProcesamientoEventArgs e)
        {
            LogError(e.NombreMetodo, AuthenticatedUser.Nombres, new Uri(GetUrl, UriKind.RelativeOrAbsolute), e.Error);
        }


        private static string GetUrl
        {
            get { return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath; }

        }
        

        protected bool VerificarPermisos(IEnumerable<TBL_Admin_Roles> roles)
        {
            return roles.Any(rol => Context.User.IsInRole(rol.NombreRol));
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="nombrePlantilla"></param>
        /// <returns></returns>
        protected static string IconoPlantilla(string nombrePlantilla)
        {
            if (string.IsNullOrEmpty(nombrePlantilla)) return string.Empty;
            var fi = new FileInfo(nombrePlantilla);
            string rutaIcono;
            switch (fi.Extension.ToLower())
            {
                case ".doc":
                    rutaIcono = "~/Resources/images/doc.gif";
                    break;
                case ".docx":
                    rutaIcono = "~/Resources/images/doc.gif";
                    break;
                case ".rtf":
                    rutaIcono = "~/Resources/images/doc.gif";
                    break;
                case ".xls":
                    rutaIcono = "~/Resources/images/xls.gif";
                    break;
                case ".xlsx":
                    rutaIcono = "~/Resources/images/xls.gif";
                    break;
                case ".csv":
                    rutaIcono = "~/Resources/images/xls.gif";
                    break;
                case ".pdf":
                    rutaIcono = "~/Resources/images/pdf.gif";
                    break;
                case ".ppt":
                    rutaIcono = "~/Resources/images/ppt.gif";
                    break;
                case ".xml":
                    rutaIcono = "~/Resources/images/xml.gif";
                    break;
                case ".zip":
                    rutaIcono = "~/Resources/images/zip.gif";
                    break;
                case ".txt":
                    rutaIcono = "~/Resources/images/txt.gif";
                    break;
                case ".png":
                    rutaIcono = "~/Resources/images/png.png";
                    break;
                case ".gif":
                    rutaIcono = "~/Resources/images/gif.png";
                    break;
                case ".jpg":
                    rutaIcono = "~/Resources/images/jpg.png";
                    break;
                default:
                    rutaIcono = "~/Resources/images/file.gif";
                    break;
            }
            return rutaIcono;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="documento"></param>
        /// <param name="fileName"></param>
        /// <param name="strType"></param>
        protected static void DownloadDocument(byte[] documento, string fileName, string strType)
        {

            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = strType;
            HttpContext.Current.Response.AppendHeader("content-disposition", "attachment; filename=" + fileName);
            HttpContext.Current.Response.BinaryWrite(documento);
            HttpContext.Current.Response.End();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        protected static string ContenType(string extension)
        {
            var result = "";
            switch (extension)
            {
                case ".doc":
                    result = "application/ms-word";
                    break;
                case ".docx":
                    result = "application/ms-word";
                    break;
                case ".rtf":
                    result = "text/richtext";
                    break;
                case ".xls":
                    result = "application/vnd.xls";
                    break;
                case ".xlsx":
                    result = "application/vnd.xlsx";
                    break;
                case ".csv":
                    result = "application/CSV";
                    break;
                case ".pdf":
                    result = "application/pdf";
                    break;
                case ".ppt":
                    result = "application/vnd.ms-powerpoint";
                    break;
                case ".xml":
                    result = "text/xml";
                    break;
                case ".zip":
                    result = "application/x-zip-compressed";
                    break;
                case ".txt":
                    result = "text/plain";
                    break;
                case ".jpg":
                    result = "image/jpeg";
                    break;
                case ".tiff":
                    result = "image/tiff";
                    break;
                case ".png":
                    result = "image/png";
                    break;
                case ".html":
                    result = "Application/HTML";
                    break;
                default:
                    result = "text/plain";
                    break;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctrl"></param>
        protected void RegistrarControlScriptManager(Control ctrl)
        {

            if (Page.Master == null) return;
            var sm = Page.Master.FindControl("smGeneral") as ScriptManager;
            if (sm == null) return;
            sm.RegisterPostBackControl(ctrl);
        }

        /// <summary>
        /// Corta la cadena dependiendo del numero de caracteres que se le pasen como parametro
        /// </summary>
        /// <param name="strCad"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        protected string TruncateString(string strCad, int num)
        {
            if (string.IsNullOrEmpty(strCad)) return string.Empty;
            if (strCad.Length <= num) return strCad;
            return strCad.Substring(0, num) + "...";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string GetBaseQueryString()
        {
            return String.Format("?ModuleId={0}", ModuleId);
        }

        public bool IsValidEmail(string email)
        {
            Regex regex = new Regex(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$");
            Match match = regex.Match(email);
            return match.Success;
        }
    }

    public static class VerificacionRegisterScript
    {
        public static bool IsClientScriptBlockRegistered(this ScriptManager sm, string key)
        {
            var scriptBlocks = sm.GetRegisteredClientScriptBlocks();

            return scriptBlocks.Any(rs => rs.Key == key);
        }
    }

   
}