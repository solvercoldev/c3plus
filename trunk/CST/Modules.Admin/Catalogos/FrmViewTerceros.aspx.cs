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
    public partial class FrmViewTerceros : ViewPage<FrmViewTercerosPresenter, IFrmViewTercerosView>, IFrmViewTercerosView
    {

        public event EventHandler FilterEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Administración Terceros");
        }

        protected void RptListadoItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Response.Redirect(string.Format("FrmEditTercero.aspx{0}&TemplateId={1}", GetBaseQueryString(), e.CommandArgument));
        }

        protected void BtnNewClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmEditTercero.aspx{0}", GetBaseQueryString()));
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

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

        public void GetTerceros(List<Terceros> items)
        {
            rptListado.DataSource = items;
            rptListado.DataBind(); ;
        }

        protected void RptListadoItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var rol = e.Item.DataItem as Terceros;

            if (rol == null) return;

            var cmdEditar = e.Item.FindControl("CmdEditar") as LinkButton;

            if (cmdEditar != null)
            {
                cmdEditar.CommandArgument = rol.IdTercero;
            }

        }

        protected void PgrChanged(object sender, PageChanged e)
        {
            if (FilterEvent != null)
                FilterEvent(e.CurrentPage, EventArgs.Empty);
        }
    }
}