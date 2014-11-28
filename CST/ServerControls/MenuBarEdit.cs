
/* 
 
    * BARRA DE HERRAMIENTAS GENERICA.
    * WALTER MOLANO
    * LUNES 14 DE ENERO DE 2013
    
 */

using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServerControls
{

    [DefaultProperty("Text"), ToolboxData("<{0}:Pager runat=server></{0}:Pager>")]
    public class MenuBarEdit : WebControl, INamingContainer
    {
        public delegate void ClickButtonEventHandler(object sender, ButtonBarEventargs e);
        public event ClickButtonEventHandler MenuButtonClick;

        private void OnSelectedButtonClick(ButtonBarEventargs e)
        {
            if (MenuButtonClick != null)
            {
                MenuButtonClick(this, e);
            }
        }

        #region Properties

        /// <summary>
        /// Show Button New in Menu Bar
        /// </summary>
        [Browsable(true)]
        public bool ShowNew
        {
            private get
            {
                return ViewState["ShowNew"] != null ? (bool)ViewState["ShowNew"] : false;
            }
            set
            {
                ViewState["ShowNew"] = value;
            }
        }

        /// <summary>
        /// Show Button Save in Menu Bar
        /// </summary>
        public bool ShowSave
        {
            private get
            {
                return ViewState["ShowSave"] != null ? (bool)ViewState["ShowSave"] : false;
            }
            set
            {
                ViewState["ShowSave"] = value;
            }
        }

        /// <summary>
        /// Show Button Delte in Menu Bar
        /// </summary>
        public bool ShowDelete
        {
            private get
            {
                return ViewState["ShowDelete"] != null ? (bool)ViewState["ShowDelete"] : false;
            }
            set
            {
                ViewState["ShowDelete"] = value;
            }
        }

        /// <summary>
        /// Show Button Close in Menu Bar
        /// </summary>
        public bool ShowClose
        {
            private get
            {
                return ViewState["ShowClose"] != null ? (bool)ViewState["ShowClose"] : false;
            }
            set
            {
                ViewState["ShowClose"] = value;
            }
        }

        /// <summary>
        /// Show Button Close in Menu Bar
        /// </summary>
        public bool ShowRecicler
        {
            private get
            {
                return ViewState["ShowRecicler"] != null ? (bool)ViewState["ShowRecicler"] : false;
            }
            set
            {
                ViewState["ShowRecicler"] = value;
            }
        }

        /// <summary>
        /// Show Button Close in Menu Bar
        /// </summary>
        public bool ShowPublish
        {
            private get
            {
                return ViewState["ShowPublish"] != null ? (bool)ViewState["ShowPublish"] : false;
            }
            set
            {
                ViewState["ShowPublish"] = value;
            }
        }

        /// <summary>
        /// Show Button Close in Menu Bar
        /// </summary>
        public bool ShowEdit
        {
            private get
            {
                return ViewState["ShowEdit"] != null ? (bool)ViewState["ShowEdit"] : false;
            }
            set
            {
                ViewState["ShowEdit"] = value;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public bool EnabledSave
        {
            private get
            {
                return ViewState["EnablesSave"] != null ? (bool)ViewState["EnablesSave"] : false;
            }
            set
            {
                ViewState["EnablesSave"] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool EnabledNew
        {
            private get
            {
                return ViewState["EnabledNew"] != null ? (bool)ViewState["EnabledNew"] : false;
            }
            set
            {
                ViewState["EnabledNew"] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool EnabledDelete
        {
            private get
            {
                return ViewState["EnabledDelete"] != null ? (bool)ViewState["EnabledDelete"] : false;
            }
            set
            {
                ViewState["EnabledDelete"] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool EnabledClose
        {
            private get
            {
                return ViewState["EnabledClose"] != null ? (bool)ViewState["EnabledClose"] : false;
            }
            set
            {
                ViewState["EnabledClose"] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool EnabledRecicler
        {
            private get
            {
                return ViewState["EnabledRecicler"] != null ? (bool)ViewState["EnabledRecicler"] : false;
            }
            set
            {
                ViewState["EnabledRecicler"] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool EnabledPublish
        {
            private get
            {
                return ViewState["EnabledPublish"] != null ? (bool)ViewState["EnabledPublish"] : false;
            }
            set
            {
                ViewState["EnabledPublish"] = value;
            }
        }

         /// <summary>
        /// 
        /// </summary>
        public bool EnabledEdit
        {
            private get
            {
                return ViewState["EnabledEdit"] != null ? (bool)ViewState["EnabledEdit"] : false;
            }
            set
            {
                ViewState["EnabledEdit"] = value;
            }
        }

        
        #endregion

        #region Metodos

       

        protected override void Render(HtmlTextWriter writer)
        {
            if (Site != null && Site.DesignMode)
            {
                writer.Write("Barra de Menus ...");
            }
            base.Render(writer);
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();
            BuildNavigationControls();
            base.CreateChildControls();
        }

        private void BuildNavigationControls()
        {

            // New
            if (ShowNew)
            {
                var newControl = CreateImageButton(ButtonAction.New, EnabledNew ? "~/Resources/Images/New.png" : "~/Resources/Images/NewDisabled.png");
                Controls.Add(newControl);
            }

            // Edit
            if (ShowEdit)
            {
                var editControl = CreateImageButton(ButtonAction.Edit, EnabledEdit ? "~/Resources/Images/edit.png" :  "~/Resources/Images/editDisabled.png");
                Controls.Add(editControl);
            }

            // Save
            if (ShowSave)
            {
                var saveControl = CreateImageButton(ButtonAction.Save, EnabledSave ? "~/Resources/Images/Save.png" : "~/Resources/Images/SaveDisabled.png");
                Controls.Add(saveControl);
            }

            // Delete
            if (ShowDelete)
            {
                var deleteControl = CreateImageButton(ButtonAction.Delete, EnabledDelete ? "~/Resources/Images/Delete.png" : "~/Resources/Images/DeleteDisabled.png");
                Controls.Add(deleteControl);
            }

            // Publish
            if (ShowPublish)
            {
                var pblishControl = CreateImageButton(ButtonAction.Publish, EnabledPublish ? "~/Resources/Images/publish.png" : "~/Resources/Images/publishDisabled.png");
                Controls.Add(pblishControl);
            }

            // Recicler
            if (ShowRecicler)
            {
                var reciclerControl = CreateImageButton(ButtonAction.Recicler, EnabledRecicler ? "~/Resources/Images/RecyceeBin.png" : "~/Resources/Images/RecyceeBinDisabled.png");
                Controls.Add(reciclerControl);
            }

            // Close
            if (ShowClose)
            {
                var closeControl = CreateImageButton(ButtonAction.Close, EnabledClose ? "~/Resources/Images/closewin.png" : "~/Resources/Images/closewinDisabled.png");
                Controls.Add(closeControl);
            }

        }

        public void ConfigurenControls()
        {

            // New
            if (ShowNew)
            {
                var imgNew = FindControl("New") as ImageButton;
                if (imgNew != null)
                {
                    imgNew.Enabled = EnabledNew;
                    imgNew.ImageUrl = EnabledNew ? "~/Resources/Images/New.png" : "~/Resources/Images/NewDisabled.png";
                }
            }

            // Edit
            if (ShowEdit)
            {
                var imgEdit = FindControl("Edit") as ImageButton;
                if (imgEdit != null)
                {
                    imgEdit.Enabled = EnabledEdit;
                    imgEdit.ImageUrl = EnabledEdit
                                           ? "~/Resources/Images/edit.png"
                                           : "~/Resources/Images/editDisabled.png";
                }
            }

            // Save
            if (ShowSave)
            {
                var imgSave = FindControl("Save") as ImageButton;
                if (imgSave != null)
                {
                    imgSave.Enabled = EnabledSave;
                    imgSave.ImageUrl = EnabledSave
                                           ? "~/Resources/Images/Save.png"
                                           : "~/Resources/Images/SaveDisabled.png";
                }
            }

            // Delete
            if (ShowDelete)
            {
                var imgDelete = FindControl("Delete") as ImageButton;
                if (imgDelete != null)
                {
                    imgDelete.Enabled = EnabledDelete;
                    imgDelete.ImageUrl = EnabledDelete
                                             ? "~/Resources/Images/Delete.png"
                                             : "~/Resources/Images/DeleteDisabled.png";
                }
            }

            // Publish
            if (ShowPublish)
            {
                var imPublish = FindControl("Publish") as ImageButton;
                if (imPublish != null)
                {
                    imPublish.Enabled = EnabledPublish;
                    imPublish.ImageUrl = EnabledPublish
                                             ? "~/Resources/Images/publish.png"
                                             : "~/Resources/Images/publishDisabled.png";
                }
            }

            // Recicler
            if (ShowRecicler)
            {

                var imRecicler = FindControl("Recicler") as ImageButton;
                if (imRecicler != null)
                {
                    imRecicler.Enabled = EnabledRecicler;
                    imRecicler.ImageUrl = EnabledRecicler
                                              ? "~/Resources/Images/RecyceeBin.png"
                                              : "~/Resources/Images/RecyceeBinDisabled.png";
                }
            }

            // Close
            if (ShowClose)
            {
             
                var imClose = FindControl("Close") as ImageButton;
                if (imClose != null)
                {
                    imClose.Enabled = EnabledClose;
                    imClose.ImageUrl = EnabledClose
                                           ? "~/Resources/Images/closewin.png"
                                           : "~/Resources/Images/closewinDisabled.png";
                }
            }
            
        }

        private Control CreateImageButton(ButtonAction buttonAction, string imageUrl)
        {
            var imgButton = new ImageButton
                                {
                                    ID = buttonAction.ToString(),
                                    ImageUrl = imageUrl,
                                    CssClass = "BtnMenuBar"
                                };
            
            switch (buttonAction)
            {
                case ButtonAction.Save:
                    imgButton.Click += BtnSaveClick;
                    imgButton.ToolTip = "Save";
                    imgButton.Enabled = EnabledSave;
                    imgButton.CausesValidation = true;
                    break;
                case ButtonAction.Delete:
                    imgButton.Click += BtnDeleteClick;
                    imgButton.ToolTip = "Delete";
                    imgButton.Enabled = EnabledDelete;
                    imgButton.CausesValidation = false;
                    break;
                case ButtonAction.New:
                    imgButton.Click += BtnNewClick;
                    imgButton.ToolTip = "New";
                    imgButton.Enabled = EnabledNew;
                    imgButton.CausesValidation = false;
                    break;
                case ButtonAction.Close:
                    imgButton.Click += BtnCloseClick;
                    imgButton.ToolTip = "Close";
                    imgButton.Enabled = EnabledClose;
                    imgButton.CausesValidation = false;
                    break;

                case ButtonAction.Recicler:
                    imgButton.Click += BtnReciclerClick;
                    imgButton.ToolTip = "Recicle";
                    imgButton.Enabled = EnabledRecicler;
                    imgButton.CausesValidation = false;
                    break;

                case ButtonAction.Publish:
                    imgButton.Click += BtnPublishClick;
                    imgButton.ToolTip = "Publish";
                    imgButton.Enabled = EnabledPublish;
                    imgButton.CausesValidation = false;
                    break;

                case ButtonAction.Edit:
                    imgButton.Click += BtnEditClick;
                    imgButton.ToolTip = "Edit";
                    imgButton.Enabled = EnabledEdit;
                    imgButton.CausesValidation = false;
                    break;
            }

            Control control = imgButton;
            return control;
        }

        
        private void BtnCloseClick(object sender, EventArgs e)
        {
            var args = new ButtonBarEventargs("Close");
            OnSelectedButtonClick(args);
        }

        private void BtnNewClick(object sender, EventArgs e)
        {
            var args = new ButtonBarEventargs("New");
            OnSelectedButtonClick(args);
            Controls.Clear();
            BuildNavigationControls();
        }

        private void BtnDeleteClick(object sender, EventArgs e)
        {
            var args = new ButtonBarEventargs("Delete");
            OnSelectedButtonClick(args);
            Controls.Clear();
            BuildNavigationControls();
        }

        private void BtnSaveClick(object sender, EventArgs e)
        {
            var args = new ButtonBarEventargs("Save");
            OnSelectedButtonClick(args);
            Controls.Clear();
            BuildNavigationControls();
        }

        private void BtnReciclerClick(object sender, EventArgs e)
        {
            var args = new ButtonBarEventargs("Recicle");
            OnSelectedButtonClick(args);
            Controls.Clear();
            BuildNavigationControls();
        }

        private void BtnPublishClick(object sender, EventArgs e)
        {
            var args = new ButtonBarEventargs("Publish");
            OnSelectedButtonClick(args);
            Controls.Clear();
            BuildNavigationControls();
        }

        private void BtnEditClick(object sender, EventArgs e)
        {
            var args = new ButtonBarEventargs("Edit");
            OnSelectedButtonClick(args);
            Controls.Clear();
            BuildNavigationControls();
        }

        private enum ButtonAction
        {
            New,
            Save,
            Delete,
            Close,
            Recicler,
            Publish,
            Edit
        }
        #endregion
   

    }

    public class ButtonBarEventargs
    {
        private string _nameButton;

        public ButtonBarEventargs(string nameButton)
        {
            _nameButton = nameButton;
        }

        public string NameButton { get { return _nameButton; } }
    }

}