using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Contratos.IViews;
using Presenters.Contratos.Presenters;
using ServerControls;

namespace Modules.Contratos.UserControls
{
    public partial class WuCLogContratos : ViewUserControl<LogContratosPresenter, ILogContratosView>, ILogContratosView
    {
        public event EventHandler FilterEvent;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RptListadoItemDatBound(object sender, RepeaterItemEventArgs e)
        {
            var log = e.Item.DataItem as LogContratos;
            if (log == null) return;

            var litDate = e.Item.FindControl("litDate") as Literal;
            if (litDate != null) litDate.Text = string.Format("{0:dd/MM/yyyy hh:mm tt}", log.CreateOn);

            var litDescripcion = e.Item.FindControl("litDescripcion") as Literal;
            if (litDescripcion != null) litDescripcion.Text = string.Format("{0}", log.Descripcion);

        }

        protected void PgrListLogPageChanged(object sender, PageChanged e)
        {
            if (FilterEvent == null) return;
            FilterEvent(e.CurrentPage, EventArgs.Empty);
        }

        public string IdContrato
        {
            get
            {
                return Request.QueryString.Get("IdContrato");
            }
        }

        public bool IsLoadedControl
        {
            get
            {
                if (ViewState["LoadedControl"] == null)
                    ViewState["LoadedControl"] = false;
                return (bool)ViewState["LoadedControl"];
            }
            set { ViewState["LoadedControl"] = value; }
        }

        public void RefreshInfo()
        {
            if (FilterEvent != null)
                FilterEvent(0, EventArgs.Empty);
        }


        public void LogsList(List<LogContratos> items)
        {
            rptListado.DataSource = items;
            rptListado.DataBind();
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public bool IsValid
        {
            get { return true; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public void CargarLog()
        {
            if (FilterEvent == null) return;
            FilterEvent(null, EventArgs.Empty);
        }
    }
}