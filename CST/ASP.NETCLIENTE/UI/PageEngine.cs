using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Core;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Util;
using Microsoft.Practices.Unity;
using Modules.Loader;

namespace ASP.NETCLIENTE.UI
{
    public class PageEngine : SolutionFrameworkPage
    {


       
        private readonly bool _shouldLoadContent;
        private readonly ModuleLoader _moduleLoader;
        private TBL_Admin_Modulos _modules;
        private readonly ISfTBL_Admin_ModulosManagementServices _moduleService;
        private BaseTemplate _templateControl;
        private readonly IDictionary<string, string> _stylesheets = new Dictionary<string, string>();
        private readonly IDictionary<string, string> _javaScripts = new Dictionary<string, string>();
        private readonly IDictionary<string, string> _metaTags = new Dictionary<string, string>();
      


        #region properties
        /// <summary>
        /// Property RootNode (Node)
        /// </summary>
        public TBL_Admin_Modulos RootNode { get; set; }

       
        /// <summary>
        /// Property ActiveSection (Section)
        /// </summary>
        public TBL_Admin_TypeByModules ActiveSection { get; set; }

        
        /// <summary>
        /// Property TemplateControl (BaseTemplate)
        /// </summary>
        private new BaseTemplate TemplateControl
        {
            get { return _templateControl; }
            set { _templateControl = value; }
        }

        #endregion

        protected PageEngine()
        {
            _templateControl = null;
            _shouldLoadContent = true;
            _moduleLoader = Container.Resolve<ModuleLoader>();
            _moduleService = Container.Resolve<ISfTBL_Admin_ModulosManagementServices>();
        }


        /// <summary>
        /// Registrando Hojas de Estilo CSS
        /// </summary>

        public void RegisterStylesheet(string key, string absoluteCssPath)
        {
            if (!_stylesheets.ContainsKey(key))
            {
                _stylesheets.Add(key, absoluteCssPath);
            }
        }

        /// <summary>
        /// Registrar javascripts.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="absoluteJavaScriptPath"></param>
        public void RegisterJavascript(string key, string absoluteJavaScriptPath)
        {
            if (!_javaScripts.ContainsKey(key))
            {
                _javaScripts.Add(key, absoluteJavaScriptPath);
            }
        }

        /// <summary>
        ///Registrando los meta Tags.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="content"></param>
        public void RegisterMetaTag(string name, string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                _metaTags[name] = content;
            }
        }

        protected override void OnInit(EventArgs e)
        {
             if (Context.Request.QueryString["ModuleId"] != null)
             {
                 var id = Convert.ToInt32(Context.Request.QueryString["ModuleId"]);
                 _modules = _moduleService.FindById(id);
             }
           
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-Es");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es-Es");

           if (_shouldLoadContent)
            {
                LoadContent();
            }
           
            base.OnInit(e);
        }

        /// <summary>
        /// Use a custom HtmlTextWriter to render the page if the url is rewritten, to correct the form action.
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            InsertStylesheets();
            InsertJavaScripts();
            InsertMetaTags();
            base.Render(writer);
        }

        private void LoadContent()
        {
            var appRoot = UrlHelper.GetApplicationPath();
            //if (_activeNode.SolutionFramework_Template != null)
            //{
            var templatePath = appRoot + "Template/WPC/Template.ascx";
                //var templatePath = appRoot + _activeNode.SolutionFramework_Template.Path;
                _templateControl = (BaseTemplate)LoadControl(templatePath);
                _templateControl.ID = "p";


                if (_modules.NombreModulo != "")
                {
                    _templateControl.Title = _modules.NombreModulo;
                }
                else
                {
                    _templateControl.Title = "PWC v1.0";
                }

                //RegisterStylesheet("maincss", appRoot + _activeNode.SolutionFramework_Template.basepath + "/Css/" + _activeNode.SolutionFramework_Template.css);
                RegisterStylesheet("maincss", appRoot + "Template/WPC/css/style.css");
                //RegisterStylesheet("maincss",  "~/Template/WPC/css/style.css");
                //RegisterJavascript("addThis", appRoot + _activeNode.SolutionFramework_Template.basepath + "/Scripts/addthis_widget.js");



                //Registrar los Modulos genericos asociados a la plantilla

            //}
            //else
            //{
            //    throw new Exception("No exoste una plantilla asociada con el nodo actual.");
            //}

            //var section = _iSfSectionManagementServices.FindSectionBySpec(1);
                foreach (var section in _modules.TBL_Admin_TypeByModules)
            {
                var moduleControl = CreateModuleControlForSection(section);
                if (moduleControl != null)
                {
                    //((PlaceHolder)_templateControl.Containers[section.placeholder]).Controls.Add(moduleControl);
                    ((PlaceHolder)_templateControl.Containers["Content_Main"]).Controls.Add(moduleControl);
                }
            }

            if (_templateControl != null)
            {
                Controls.AddAt(0, _templateControl);
                // remove html that was in the original page (Default.aspx)
                for (var i = Controls.Count - 1; i < 0; i--)
                    Controls.RemoveAt(i);
            }

        }

        private Control CreateModuleControlForSection(TBL_Admin_TypeByModules section)
        {

            // Create the module that is connected to the section.
            // var module = _moduleLoader.GetModuleFromSection("SolutionFramework.Modules.News.NewsModule,SolutionFramework.Modules.News");
            //var module = ModuleLoader.GetModuleFromSection(section);
            //if (module != null)
            //{
            //    //if (Context.Request.PathInfo.Length > 0)
            //    //{
            //    //    // Parse the PathInfo of the request because they can be the parameters 
            //    //    // for the module that is connected to the active section.
            //    //    module.ModulePathInfo = Context.Request.PathInfo;
            //    //}
            //    return LoadModuleControl(module);
            //}

            return null;
        }

        private Control LoadModuleControl(ModuleBase module)
        {
            var ctrl = LoadControl(UrlHelper.GetApplicationPath() + module.DefaultViewControlPath);
            return ctrl;
        }

        private void InsertStylesheets()
        {
            var stylesheetLinks = new List<string>(_stylesheets.Values);
            if (TemplateControl != null)
                TemplateControl.RenderCssLinks(stylesheetLinks.ToArray());
        }

        private void InsertJavaScripts()
        {
            var javaScriptLinks = new List<string>(_javaScripts.Values);
            if (TemplateControl != null)
                TemplateControl.RenderJavaScriptLinks(javaScriptLinks.ToArray());
        }

        private void InsertMetaTags()
        {
            if (TemplateControl != null)
                TemplateControl.RenderMetaTags((IDictionary)_metaTags);
        }

    }
}