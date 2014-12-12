using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Admin.IViews;
using Presenters.Admin.Presenters;
using ServerControls;

namespace Modules.Admin.Catalogos
{
    public partial class FrmViewRoles : ViewPage<FrmViewRolesPresenter, IFrmViewRolesView>, IFrmViewRolesView
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Administración Roles");
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public event EventHandler FilterEvent;

        public int TotalRegistrosPaginador
        {
            set { pgrListado.RowCount = value; }
        }

        public int PageZise
        {
            get { return pgrListado.PageSize; }
        }

        public string ModuleSetupId
        {
            get { return ViewState["ModuleSetupId"] == null ? string.Empty : ViewState["ModuleSetupId"].ToString(); }
            set { ViewState["ModuleSetupId"] = value; }
        }

        public void GetRoles(List<TBL_Admin_Roles> items)
        {
            rptListado.DataSource = items;
            rptListado.DataBind(); 
        }

        protected void RptListadoItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var rol = e.Item.DataItem as TBL_Admin_Roles;

            if (rol == null) return;

            var cmdEditar = e.Item.FindControl("CmdEditar") as LinkButton;

            if (cmdEditar != null)
            {
                cmdEditar.CommandArgument = rol.IdRol.ToString();
            }

            var chkActivo = e.Item.FindControl("chkActivo") as CheckBox;

            if (chkActivo != null)
            {
                chkActivo.Checked = rol.Activo;
            }

            var chkGrupo = e.Item.FindControl("chkGroup") as CheckBox;

            if (chkGrupo != null)
            {
                if (rol.IsGroup != null) chkGrupo.Checked = (bool) rol.IsGroup;
            }

        }

        protected void RptListadoItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Response.Redirect(string.Format("FrmEditRoles.aspx{0}&TemplateId={1}", GetBaseQueryString(), e.CommandArgument));
        }

        protected void BtnNewClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmEditRoles.aspx{0}", GetBaseQueryString()));
        }

        protected void PgrChanged(object sender, PageChanged e)
        {
            if (FilterEvent != null)
                FilterEvent(e.CurrentPage, EventArgs.Empty);
        }
    }
}