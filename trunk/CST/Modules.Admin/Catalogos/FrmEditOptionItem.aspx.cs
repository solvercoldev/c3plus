using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Admin.IViews;
using Presenters.Admin.Presenters;
using System.Collections;
using System.Linq;

namespace Modules.Admin.Catalogos
{
    public partial class FrmEditOptionItem : ViewPage<EditOptionListPresenter, IEditOptionListView>, IEditOptionListView
	{
        #region Delegates

        public event EventHandler SaveEvent;
        public event EventHandler DeleteEvent;

        #endregion

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Editar Option List Item");

            // btnEliminar.Visible = !string.IsNullOrEmpty(IdCategoriaReclamo);
            btnSave.Visible = !string.IsNullOrEmpty(IdOpcion);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            HideControlsevent += FrmEditUserControlsevent;
        }

        void FrmEditUserControlsevent(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnEliminar.Visible = false;
        }

        #endregion


        #region events

        protected void BtnSaveClick(object sender, EventArgs e)
        {
            if (SaveEvent != null)
            {
                SaveEvent(null, EventArgs.Empty);
                Response.Redirect(string.Format("FrmViewOptionList.aspx{0}&OptionListId={1}", GetBaseQueryString(), this.IdOpcion));
            }
        }

        protected void BtnDeleteClick(object sender, EventArgs e)
        {
            if (DeleteEvent != null)
                DeleteEvent(null, EventArgs.Empty);
        }

        protected void BtnBackClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmAdminOptionsList.aspx{0}", GetBaseQueryString()));
        }

        #endregion

        #region Members


        public string IdModulo
        {
            set { txtModule.Text = value; }
        }

        public string descripcion
        {
            set { txtDescripcion.Text = value; }
            get { return txtDescripcion.Text; }
        }

        public string key
        {
            set { txtKey.Text = value; }
        }

        public string value
        {
            set { txtValue.Text = value; }
            get { return txtValue.Text; }
        }

        public bool Activo
        {
            set { chkActive.Checked = value; }
            get { return chkActive.Checked; }
        }

        public string CreateBy
        {
            set { lblCreateBy.Text = value; }
        }

        public string CreateOn
        {
            set { lblCreateOn.Text = value; }
        }

        public string ModifiedBy
        {
            set { lblModifiedBy.Text = value; }
        }

        public string ModifiedOn
        {
            set { lblModifiedOn.Text = value; }
        }

        public string IdOpcion
        {
            get { return Request.QueryString["OptionListId"]; }
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        #endregion

   
	}
}