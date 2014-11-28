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
    public partial class FrmAdminOptionsList : ViewPage<AdminOptionsListPresenters,IAdminOptionList>,IAdminOptionList
    {
        #region Delegates

        public event EventHandler FilterEvent;
        public event EventHandler PagerEvent;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Administrador de lista de opciones");
        }

        protected void BtnNewClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmAddOptionItem.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnFilterReclamos_Click(object sender, EventArgs e)
        {
            if (FilterEvent != null)
                FilterEvent(sender, EventArgs.Empty);
        }

        protected void PgrChanged(object sender, PageChanged e)
        {
            if (PagerEvent != null)
                PagerEvent(e.CurrentPage, EventArgs.Empty);
        }

        public void GetOptionsList(List<TBL_Admin_OptionList> items)
        {
            rptListado.DataSource = items;
            rptListado.DataBind();
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

        protected void RptListadoItemCommand(object source, RepeaterCommandEventArgs e)
        {

            Response.Redirect(string.Format("FrmViewOptionList.aspx{0}&OptionListId={1}", GetBaseQueryString(), e.CommandArgument));
        }

        protected void RptListadoItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var Option = e.Item.DataItem as TBL_Admin_OptionList;

            if (Option == null) return;

            var cmdEditar = e.Item.FindControl("CmdEditar") as LinkButton;

            if (cmdEditar != null)
            {
                cmdEditar.CommandArgument = Option.IdOpcion.ToString();
            }

            var litModulo = e.Item.FindControl("litModulo") as Literal;
            if (litModulo != null)
            {
                litModulo.Text = Option == null ? string.Empty : Option.IdModule.ToString();
            }

            var litKey = e.Item.FindControl("litKey") as Literal;
            if (litKey != null)
            {
                litKey.Text = Option == null ? string.Empty : Option.Key;
            }


            var litValue = e.Item.FindControl("litValue") as Literal;
            if (litValue != null)
            {
                litValue.Text = Option == null ? string.Empty : Option.Value;
            }

            var litDescripcion = e.Item.FindControl("litDescripcion") as Literal;
            if (litDescripcion != null)
            {
                litDescripcion.Text = Option == null ? string.Empty : Option.Descripcion;
            }
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string Search
        {
            get { return txtSearch.Text; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }
    }
}