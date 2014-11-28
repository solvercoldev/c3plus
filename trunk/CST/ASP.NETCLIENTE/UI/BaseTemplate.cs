
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ASP.NETCLIENTE.UI
{
    public abstract class BaseTemplate : UserControl
    {
        /// <summary>
        /// Template controls that inherit from BaseTemplate must have a Literal control with id="PageTitle".
        /// </summary>
        protected Literal PageTitle;
        /// <summary>
        /// Template controls that inherit from BaseTemplate must have a Literal control with 
        /// id="Stylesheets".
        /// </summary>
        protected Literal Stylesheets;
        /// <summary>
        /// Template controls that inherit from BaseTemplate must have a Literal control with 
        /// id="Javascripts".
        /// </summary>
        protected Literal JavaScripts;
        /// <summary>
        /// Template controls that inherit from BaseTemplate must have a Literal control with 
        /// id="MetaTags".
        /// </summary>
        protected Literal MetaTags;

        /// <summary>
        /// The page title as shown in the title bar of the browser.
        /// </summary>
        public string Title
        {
            get { return PageTitle.Text; }
            set { PageTitle.Text = value; }
        }

        /// <summary>
        /// The form of the template.
        /// </summary>
        private Control Form
        {
            get
            { 
                return Controls.OfType<HtmlForm>().Select(ctrl => ctrl).FirstOrDefault(); 
            }
        }

        /// <summary>
        /// All content containers.
        /// </summary>
        public Hashtable Containers
        {
            get
            {
                var tbl = new Hashtable();
                foreach (Control ctrl in Form.Controls)
                {
                    if (ctrl is PlaceHolder)
                    {
                        tbl.Add(ctrl.ID, ctrl);
                    }
                    // Also check for user controls with content placeholders.
                    else if (ctrl is UserControl)
                    {
                        foreach (var ctrl2 in ctrl.Controls.OfType<PlaceHolder>())
                        {
                            tbl.Add(ctrl2.ID, ctrl2);
                        }
                    }
                }
                return tbl;
            }
        }

        /// <summary>
        /// Converts the list of css links to stylesheet tags and inserts these in the appropriate place.
        /// </summary>
        /// <param name="stylesheets"></param>
        public void RenderCssLinks(IEnumerable<string> stylesheets)
        {
            var sb = new StringBuilder();
            foreach (var stylesheet in stylesheets)
            {
                sb.AppendFormat("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" />\n\t", stylesheet);
            }
            Stylesheets.Text = sb.ToString();
        }

        /// <summary>
        /// Converts the list of javaScripts to script tags and inserts these in the appropriate place.
        /// </summary>
        public void RenderJavaScriptLinks(IEnumerable<string> javaScripts)
        {
            var sb = new StringBuilder();
            foreach (var javaScript in javaScripts)
            {
                sb.AppendFormat("<script src=\"{0}\" type=\"text/javascript\"></script>\n\t", javaScript);
            }
            JavaScripts.Text = sb.ToString();
        }

        /// <summary>
        /// Converts the dictionary of meta tags to real meta tags and inserts these in the appropriate place.
        /// </summary>
        /// <param name="metaTags"></param>
        public void RenderMetaTags(IDictionary metaTags)
        {
            var sb = new StringBuilder();
            if (metaTags != null)
            {
                foreach (DictionaryEntry entry in metaTags)
                {
                    sb.AppendFormat("<meta name=\"{0}\" content=\"{1}\" />\n\t", entry.Key, entry.Value);
                }
                MetaTags.Text = sb.ToString();
            }
        }

        /// <summary>
        /// Insert hyperlinks in the placeholders to enable placeholder selection (for administration only).
        /// </summary>
        public void InsertContainerButtons()
        {
            var placeholderChooseControl = Context.Request.QueryString["Control"];
            if (placeholderChooseControl == null) return;
            foreach (PlaceHolder plc in Containers.Values)
            {
                var btn = new HtmlInputButton {Value = plc.ID};
                btn.Attributes.Add("onclick", String.Format("window.opener.setPlaceholderValue('{0}','{1}');self.close()", placeholderChooseControl, plc.ID));
                plc.Controls.Add(btn);
            }
        }
    }
}
