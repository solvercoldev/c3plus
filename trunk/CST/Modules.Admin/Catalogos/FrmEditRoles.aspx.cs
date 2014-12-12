using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Admin.IViews;
using Presenters.Admin.Presenters;

namespace Modules.Admin.Catalogos 
{
    public partial class FrmEditRoles : ViewPage<FrmEditRolesPresenter, IFrmEditRolesView>, IFrmEditRolesView
    {
        public event EventHandler SaveEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler ActualizarEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.IsNullOrEmpty(IdRol) ? "Nuevo Rol" : "Editar Rol");
            btnAct.Visible = !string.IsNullOrEmpty(IdRol);
            btnSave.Visible = string.IsNullOrEmpty(IdRol);
            txtIdRol.Visible = !string.IsNullOrEmpty(IdRol);
            txtIdRol.Enabled = string.IsNullOrEmpty(IdRol);
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public bool Activo
        {
            get { return chkActive.Checked; }
            set { chkActive.Checked = value; }
        }

        public bool Grupo
        {
            get { return chkGrupo.Checked; }
            set { chkGrupo.Checked = value; }
        }

        public string NombreRol
        {
            get { return txtNombreRol.Text; }
            set { txtNombreRol.Text = value; }
        }

        public string IdRol
        {
            get { return string.IsNullOrEmpty(Request.QueryString["TemplateId"]) ? txtIdRol.Text : Request.QueryString["TemplateId"]; }
            set { txtIdRol.Text = value; }
        }

        public String CreatedBy
        {
            set { LitCreatedBy.Text = value; }
        }

        public string CreatedOn
        {
            set { LitCreatedOn.Text = value; }
        }

        public String ModifiedBy
        {
            set { LiModifiedBy.Text = value; }
        }

        public string ModifiedOn
        {
            set { LiModifiedOn.Text = value; }
        }

        protected void BtnBackClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmViewRoles.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnSaveClick(object sender, EventArgs e)
        {
            if (SaveEvent != null)
                SaveEvent(null, EventArgs.Empty);

            Response.Redirect(string.Format("FrmViewRoles.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnActClick(object sender, EventArgs e)
        {
            if (ActualizarEvent != null)
                ActualizarEvent(null, EventArgs.Empty);

            Response.Redirect(string.Format("FrmViewRoles.aspx{0}", GetBaseQueryString()));
        }
    }
}