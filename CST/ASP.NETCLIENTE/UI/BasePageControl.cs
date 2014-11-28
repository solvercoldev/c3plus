using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ASP.NETCLIENTE.UI
{
    public abstract class BasePageControl : UserControl
    {
        /// <summary>
        /// Template controls that inherit from BasePageControl must have a Literal control with id="PageTitle"
        /// </summary>
        protected Literal PageTitle;
        /// <summary>
        /// Template controls that inherit from BasePageControl must have a HtmlControl control id="CssStyleSheet"
        /// </summary>
        protected HtmlControl CssStyleSheet;
        /// <summary>
        /// Template controls that inherit from BasePageControl must have a PlaceHolder control id="PageContent"
        /// </summary>
        protected PlaceHolder PageContent;


        /// <summary>
        /// Template controls that inherit from BasePageControl must have a PlaceHolder control id="AddedCssPlaceHolder"
        /// </summary>
        protected PlaceHolder AddedCssPlaceHolder;
        /// <summary>
        /// Template controls that inherit from BasePageControl must have a PlaceHolder control id="AddedJavaScriptPlaceHolder"
        /// </summary>
        protected PlaceHolder AddedJavaScriptPlaceHolder;

        private readonly OrderedDictionary _stylesheets;
        private readonly OrderedDictionary _javascripts;

        protected BasePageControl()
        {
            _stylesheets = new OrderedDictionary();
            _javascripts = new OrderedDictionary();
        }

        /// <summary>
        /// The page title as shown in the title bar of the browser
        /// </summary>
        public string Title
        {
            get
            {
                return PageTitle.Text;
            }
            set
            {
                PageTitle.Text = value;
            }
        }

        #region Register Javascript and CSS
        /// <summary>
        /// Register stylesheets.
        /// </summary>
        /// <param name="key">The unique key for the stylesheet. Note that Cuyahoga already uses 'maincss' as key.</param>
        /// <param name="absoluteCssPath">The path to the css file from the application root (starting with /).</param>
        public void RegisterStylesheet(string key, string absoluteCssPath)
        {
            if (_stylesheets[key] == null)
            {
                _stylesheets.Add(key, absoluteCssPath);
            }
        }

        /// <summary>
        /// Register javascripts.
        /// </summary>
        /// <param name="key">The unique key for the stylesheet. Note that Cuyahoga already uses 'maincss' as key.</param>
        /// <param name="absoluteJavascriptPath">The path to the css file from the application root (starting with /).</param>
        public void RegisterJavascript(string key, string absoluteJavascriptPath)
        {
            if (_javascripts[key] == null)
            {
                _javascripts.Add(key, absoluteJavascriptPath);
            }
        }

        public void InsertStylesheets()
        {
            var stylesheetlinks = new string[_stylesheets.Count];
            var i = 0;
            foreach (string stylesheet in _stylesheets.Values)
            {
                stylesheetlinks[i] = stylesheet;
                i++;
            }
            RenderCssLinks(stylesheetlinks);
        }

        public void InsertJavascripts()
        {
            var javascriptlinks = new string[_javascripts.Count];
            var i = 0;
            foreach (string javascript in _javascripts.Values)
            {
                javascriptlinks[i] = javascript;
                i++;
            }
            RenderJavascriptLinks(javascriptlinks);
        }

        /// <summary>
        /// Converts the list of css links to stylesheet tags and inserts these in the appropriate place.
        /// </summary>
        /// <param name="stylesheets"></param>
        private void RenderCssLinks(IEnumerable<string> stylesheets)
        {
            var sb = new StringBuilder();
            foreach (var stylesheet in stylesheets)
            {
                var css = new HtmlLink { Href = stylesheet };
                css.Attributes["rel"] = "stylesheet";
                css.Attributes["type"] = "text/css";
                css.Attributes["media"] = "all";
                AddedCssPlaceHolder.Controls.Add(css);
            }
        }

        /// <summary>
        /// Converts the list of javascript links to stylesheet tags and inserts these in the appropriate place.
        /// </summary>
        /// <param name="javascripts"></param>
        public void RenderJavascriptLinks(IEnumerable<string> javascripts)
        {
            var sb = new StringBuilder();
            foreach (var javascript in javascripts)
            {
                var js = new HtmlGenericControl { TagName = "script" };
                js.Attributes["src"] = javascript;
                js.Attributes["type"] = "text/javascript";
                AddedJavaScriptPlaceHolder.Controls.Add(js);
            }
        }


        #endregion Register Javascript and CSS

        /// <summary>
        /// Path to external stylesheet file, relative from the application root.
        /// </summary>
        public string Css
        {
            get
            {
                return CssStyleSheet.Attributes["href"];
            }
            set
            {
                CssStyleSheet.Attributes.Add("href", ResolveUrl(value));
            }
        }

        /// <summary>
        /// Placeholder for the actual page content.
        /// </summary>
        public PlaceHolder Content
        {
            get
            {
                return PageContent;
            }
            set
            {
                PageContent = value;
            }
        }
    }
}