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
    public partial class FrmViewOptionList : ViewPage<DetailOptionListPresenter,IDetailOptionListView>,IDetailOptionListView
    {
        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {

            ImprimirTituloVentana("Detalle Option List");
            btnEdit.Visible = !string.IsNullOrEmpty(IdOpcion);

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            HideControlsevent += FrmViewUserControlsevent;
        }

        void FrmViewUserControlsevent(object sender, EventArgs e)
        {
            btnEdit.Visible = false;
        }

        #endregion

        #region Events

        protected void BtnBackClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmAdminOptionsList.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnEditClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmEditOptionItem.aspx{0}&OptionListId={1}", GetBaseQueryString(), IdOpcion));
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
        }

        public string key
        {
            set { txtKey.Text = value; }
        }

        public string value
        {
            set { txtValue.Text = value; }
        }

        public bool Activo
        {
            set { chkActive.Checked = value; }
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