﻿using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Admin.IViews;
using Presenters.Admin.Presenters;
using ServerControls;


namespace Modules.Admin.Catalogos
{
    public partial class FrmViewCampos : ViewPage<FrmViewCamposPresenter, IFrmViewCamposView>, IFrmViewCamposView
    {
        public event EventHandler FilterEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Administración Campos");
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

        public void GetCampos(List<Campos> items)
        {
            rptListado.DataSource = items;
            rptListado.DataBind();
        }

        protected void PgrChanged(object sender, PageChanged e)
        {
            if (FilterEvent != null)
                FilterEvent(e.CurrentPage, EventArgs.Empty);
        }

        protected void RptListadoItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Response.Redirect(string.Format("FrmEditCampos.aspx{0}&TemplateId={1}", GetBaseQueryString(), e.CommandArgument));
        }

        protected void BtnNewClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmEditCampos.aspx{0}", GetBaseQueryString()));
        }

        protected void RptListadoItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var rol = e.Item.DataItem as Campos;

            if (rol == null) return;

            var cmdEditar = e.Item.FindControl("CmdEditar") as LinkButton;

            if (cmdEditar != null)
            {
                cmdEditar.CommandArgument = rol.IdCampo;
            }

            var chkActivo = e.Item.FindControl("chkActivo") as CheckBox;

            if (chkActivo != null)
            {
                chkActivo.Checked = rol.IsActive;
            }
        }
    }
}