using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Modules.Contratos.UI;
using Presenters.Contratos.IViews;
using Presenters.Contratos.Presenters;

namespace Modules.Contratos.UserControls
{
    public partial class WuCAdminRadicadosContrato : ViewUserControl<AdminRadicadosContratoPresenter, IAdminRadicadosContratoView>, IAdminRadicadosContratoView, IContratoWebUserControl
    {
        #region Members

        #endregion

        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        #endregion

        #region Buttons

        protected void BtnAddRadicado_Click(object sender, EventArgs e)
        {
            var localUrl = Request.Url.AbsoluteUri;
            var fromType = "contrato";

            if (localUrl.Contains("FrmManageFasesContrato"))
                fromType = "fases";
            else if (localUrl.Contains("FrmContrato"))
                fromType = "contrato";

            Response.Redirect(string.Format("../Admin/FrmNewRadicadoContrato.aspx?ModuleId={0}&IdContrato={1}&from={2}", ModuleId, IdContrato, fromType));
        }

        protected void BtnSelectRadicado_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            var localUrl = Request.Url.AbsoluteUri;
            var fromType = "contrato";

            if (localUrl.Contains("FrmManageFasesContrato"))
                fromType = "fases";
            else if (localUrl.Contains("FrmContrato"))
                fromType = "contrato";

            Response.Redirect(string.Format("../Admin/FrmAdminRadicadoContrato.aspx?ModuleId={0}&IdContrato={1}&IdRadicado={2}&from={3}", ModuleId, IdContrato, btn.CommandArgument, fromType));
        }


        #endregion

        #region DropDownList

        protected void FilterEvent_Changed(object sender, EventArgs e)
        {
            Presenter.LoadRadicados();
        }

        #endregion

        #region Repeaters

        protected void RptRadicadosList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (Radicados)(e.Item.DataItem);
                // Bindind data

                var lblTipo = e.Item.FindControl("lblTipo") as Label;
                if (lblTipo != null) lblTipo.Text = string.Format("{0}", item.TipoRadicado);

                var lblAsunto = e.Item.FindControl("lblAsunto") as Label;
                if (lblAsunto != null) lblAsunto.Text = string.Format("{0}", item.Asunto);

                var lblNumeroRadicado = e.Item.FindControl("lblNumeroRadicado") as Label;
                if (lblNumeroRadicado != null) lblNumeroRadicado.Text = string.Format("{0}", item.Numero);

                var lblEnviadoPor = e.Item.FindControl("lblEnviadoPor") as Label;
                if (lblEnviadoPor != null) lblEnviadoPor.Text = string.Format("{0}", item.IdFromExterno);

                var lblFechaCumplimiento = e.Item.FindControl("lblFechaCumplimiento") as Label;
                if (lblFechaCumplimiento != null) lblFechaCumplimiento.Text = string.Format("{0:dd/MM/yyyy}", item.FechaReciboSalida);

                var lblEstado = e.Item.FindControl("lblEstado") as Label;
                if (lblEstado != null) lblEstado.Text = string.Format("{0}", item.EstadoRadicado);

                var lblResponsable = e.Item.FindControl("lblResponsable") as Label;
                if (lblResponsable != null) lblResponsable.Text = string.Format("{0}", item.TBL_Admin_Usuarios2.Nombres);

                var imgSelectRadicado = e.Item.FindControl("imgSelectRadicado") as ImageButton;
                if (imgSelectRadicado != null) imgSelectRadicado.CommandArgument = string.Format("{0}", item.IdRadicado);
            }
        }

        #endregion

        #endregion

        #region Methods

        public void LoadControlData()
        {
            Presenter.LoadInit();
        }

        #endregion

        #region View Members

        #region Methods

        public void LoadRadicados(List<Radicados> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.FechaReciboSalida).ToList();
            }
            rptRadicadosList.DataSource = items;
            rptRadicadosList.DataBind();
        }

        #endregion

        #region Properties

        public event Action RiseFatherPostback;

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public string IdContrato
        {
            get { return Request.QueryString.Get("IdContrato"); }
        }

        public string TipoRadicado
        {
            get
            {
                return ddlTipoRadicado.SelectedValue;
            }
            set
            {
                ddlTipoRadicado.SelectedValue = value;
            }
        }

        public string EstadoRadicado
        {
            get
            {
                return ddlEstadoRadicado.SelectedValue;
            }
            set
            {
                ddlEstadoRadicado.SelectedValue = value;
            }
        }

        public string SearchText
        {
            get
            {
                return txtSearchFilter.Text;
            }
            set
            {
                txtSearchFilter.Text = value;
            }
        }

        public bool CanAddRadicados
        {
            get
            {
                return btnAddRadicado.Visible;
            }
            set
            {
                btnAddRadicado.Visible = value;
            }
        }

        #endregion

        #endregion
    }
}