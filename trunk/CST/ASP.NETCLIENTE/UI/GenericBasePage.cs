using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ASP.NETCLIENTE.UI
{
    public class GenericBasePage : SolutionFrameworkPage
    {

        // Member variables
        private string _templateFilename;
        private string _title;
        private string _css;
        private string _templateDir;
        private BasePageControl _pageControl;

        #region properties
        /// <summary>
        /// Template property (filename of the User Control). This property can be used to change 
        /// page templates run-time.
        /// </summary>
        protected string TemplateFilename
        {
            set { _templateFilename = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected string TemplateDir
        {
            set { _templateDir = value; }
        }

        /// <summary>
        /// The page title as shown in the title bar of the browser.
        /// </summary>
        protected new string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// Path to external stylesheet file, relative from the application root.
        /// </summary>
        protected string Css
        {
            get { return _css; }
            set { _css = value; }
        }

        /// <summary>
        /// Property for the template control. This property can be used for finding other controls.
        /// </summary>
        public new UserControl TemplateControl
        {
            get
            {
                if (Controls.Count > 0)
                {
                    if (Controls[0] is UserControl)
                    {
                        return (UserControl)Controls[0];
                    }
                    return null;
                }
                return null;
            }
        }

        /// <summary>
        /// The messagebox.
        /// </summary>
        private HtmlGenericControl MessageBox
        {
            get
            {
                if (TemplateControl != null)
                {
                    return TemplateControl.FindControl("MessageBox") as HtmlGenericControl;
                }
                return null;
            }
        }

        private HtmlGenericControl ContentMessageBox
        {
            get
            {
                if (TemplateControl != null)
                {
                    return TemplateControl.FindControl("DivContent") as HtmlGenericControl;
                }
                return null;
            }
        }

        private HtmlGenericControl MessageBoxIcon
        {
            get
            {
                if (TemplateControl != null)
                {
                    return TemplateControl.FindControl("DivMessageBoxIcon") as HtmlGenericControl;
                }
                return null;
            }
        }

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        protected GenericBasePage()
        {
            _templateFilename = null;
            _templateDir = null;
            _css = null;
        }

        /// <summary>
        /// Protected constructor that accepts template parameters. Could be handled more elegantly.
        /// </summary>
        /// <param name="templateFileName"></param>
        /// <param name="templateDir"></param>
        /// <param name="css"></param>
        protected GenericBasePage(string templateFileName, string templateDir, string css)
        {
            _templateFilename = templateFileName;
            _templateDir = templateDir;
            _css = css;
        }

        /// <summary>
        /// Try to find the MessageBox control, insert the errortext and set visibility to true.
        /// </summary>
        /// <param name="errorText"></param>
        protected void ShowError(string errorText)
        {
            if (MessageBox == null || ContentMessageBox == null || MessageBoxIcon == null)
            {
                throw new Exception(errorText);
            }
            ContentMessageBox.InnerHtml = "Notificación: " + errorText;
            ContentMessageBox.Attributes["class"] = "errorbox";
            MessageBoxIcon.Attributes["class"] = "IconBoxError";
            MessageBox.Attributes["class"] = "errorbox";
            MessageBox.Visible = true;

        }

        /// <summary>
        /// Try to find the MessageBox control, insert the message and set visibility to true.
        /// </summary>
        /// <param name="message"></param>
        protected void ShowMessageOk(string message)
        {

            if (MessageBox == null) return;
            if (ContentMessageBox == null) return;
            ContentMessageBox.InnerHtml = message;
            ContentMessageBox.Attributes["class"] = "messagebox";
            MessageBoxIcon.Attributes["class"] = "IconBoxOk";
            MessageBox.Attributes["class"] = "messagebox";
            MessageBox.Visible = true;

        }

        /// <summary>
        /// Show the message of the exception, and the messages of the inner exceptions.
        /// </summary>
        /// <param name="exception"></param>
        protected void ShowException(Exception exception)
        {
            var exceptionMessage = "<p>" + exception.Message + "</p>";
            var innerException = exception.InnerException;
            while (innerException != null)
            {
                exceptionMessage += "<p>" + innerException.Message + "</p>";
                innerException = innerException.InnerException;
            }
            ShowError(exceptionMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            // Init
            PlaceHolder plc;
            var col = Controls;

            // se busca el diretorio del template
            if (_templateDir == null && ConfigurationManager.AppSettings["TemplateDir"] != null)
            {
                _templateDir = ConfigurationManager.AppSettings["TemplateDir"];
            }

            // validacion de la variable
            if (_templateFilename == null)
            {
                _templateFilename = ConfigurationManager.AppSettings["DefaultTemplate"];
            }

            var strPath = ResolveUrl(_templateDir + _templateFilename);
            //Se carga el control
            _pageControl = (BasePageControl)LoadControl(strPath);


            // Add the pagecontrol on top of the control collection of the page
            _pageControl.ID = "p";
            col.AddAt(0, _pageControl);

            // Get the Content placeholder
            plc = _pageControl.Content;
            if (plc != null)
            {
                // Iterate through the controls in the page to find the form control.
                foreach (Control control in col)
                {
                    if (control is HtmlForm)
                    {
                        // We've found the form control. Now move all child controls into the placeholder.
                        var formControl = (HtmlForm)control;
                        while (formControl.Controls.Count > 0)
                            _pageControl.Controls.Add(formControl.Controls[0]);
                    }
                }
                // throw away all controls in the page, except the page control 
                while (col.Count > 1)
                    col.Remove(col[1]);
            }
            base.OnInit(e);
        }

        #region Register Javascript and CSS
        /// <summary>
        /// Register module-specific stylesheets.
        /// </summary>
        /// <param name="key">The unique key for the stylesheet. Note that Cuyahoga already uses 'maincss' as key.</param>
        /// <param name="absoluteCssPath">The path to the css file from the application root (starting with /).</param>
        protected void RegisterStylesheet(string key, string absoluteCssPath)
        {
            //BasePageControl pageControl = (BasePageControl)this.Controls[0];
            _pageControl.RegisterStylesheet(key, absoluteCssPath);
        }

        /// <summary>
        /// Register module-specific javascripts.
        /// </summary>
        /// <param name="key">The unique key for the javascrip. </param>
        /// <param name="absoluteJavascriptPath">The path to the css file from the application root (starting with /).</param>
        protected void RegisterJavascript(string key, string absoluteJavascriptPath)
        {
            //BasePageControl pageControl = (BasePageControl)this.Controls[0];
            _pageControl.RegisterJavascript(key, absoluteJavascriptPath);
        }
        #endregion Register Javascript and CSS

        /// <summary>
        /// Use the PreRender event to set the page title and stylesheet. These are properties of the page control
        /// which is at position 0 in the controls collection.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            //Removed for CSS and Javascript Registration
            //BasePageControl pageControl = (BasePageControl)this.Controls[0];

             //Set the stylesheet and title properties
            //if (_css == null)
            //{
            //    _css = ResolveUrl(ConfigurationManager.AppSettings["DefaultCss"]);
            //}
            //if (_title == null)
            //{
            //    _title = ConfigurationManager.AppSettings["DefaultTitle"];
            //}
            _pageControl.Title = _title;
            _pageControl.Css = _css;

            _pageControl.InsertStylesheets();
            _pageControl.InsertJavascripts();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            var stringBuilder = new StringBuilder();
            var stringWriter = new StringWriter(stringBuilder);
            var htmlWriter = new HtmlTextWriter(stringWriter);
            base.Render(htmlWriter);
            var html = stringBuilder.ToString();
            var start = html.IndexOf("<form name=\"") + 12;
            var end = html.IndexOf("\"", start);
            var formId = html.Substring(start, end - start);
            var replace = formId.Replace(":", "_");
            html = html.Replace("document." + formId, "document." + replace);
            writer.Write(html);
        }



    }
}