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
    public partial class WuCAdminCompromisosContrato : ViewUserControl<AdminCompromisosContratoPresenter, IAdminCompromisosContratoView>, IAdminCompromisosContratoView, IContratoWebUserControl
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

        protected void BtnAddCompromiso_Click(object sender, EventArgs e)
        {
            var localUrl = Request.Url.AbsoluteUri;
            var fromType = "contrato";

            if (localUrl.Contains("FrmManageFasesContrato"))
                fromType = "fases";
            else if (localUrl.Contains("FrmContrato"))
                fromType = "contrato";

            Response.Redirect(string.Format("../Admin/FrmNewCompromisoContrato.aspx?ModuleId={0}&IdContrato={1}&from={2}", ModuleId, IdContrato, fromType));
        }

        protected void BtnSelectCompromiso_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            var localUrl = Request.Url.AbsoluteUri;
            var fromType = "contrato";

            if (localUrl.Contains("FrmManageFasesContrato"))
                fromType = "fases";
            else if (localUrl.Contains("FrmContrato"))
                fromType = "contrato";

            Response.Redirect(string.Format("../Admin/FrmAdminCompromisoContrato.aspx?ModuleId={0}&IdContrato={1}&IdCompromiso={2}&from={3}", ModuleId, IdContrato, btn.CommandArgument, fromType));
        }
        

        #endregion

        #region DropDownList

        protected void DdlFases_IndexChanged(Object sender, EventArgs e)
        {
            Presenter.LoadCompromisos();
        }

        #endregion

        #region Repeaters

        protected void RptCompromisosList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (Compromisos)(e.Item.DataItem);
                // Bindind data

                var lblFase = e.Item.FindControl("lblFase") as Label;
                if (lblFase != null) lblFase.Text = string.Format("Fase {0}", item.Fases.NumeroFase);

                var lblDescripcion = e.Item.FindControl("lblDescripcion") as Label;
                if (lblDescripcion != null) lblDescripcion.Text = string.Format("{0}", item.Descripcion);

                var lblFechaCumplimiento = e.Item.FindControl("lblFechaCumplimiento") as Label;
                if (lblFechaCumplimiento != null) lblFechaCumplimiento.Text = string.Format("{0:dd/MM/yyyy}", item.FechaCumplimiento);

                var lblEstado = e.Item.FindControl("lblEstado") as Label;
                if (lblEstado != null) lblEstado.Text = string.Format("{0}", item.Estado);

                var lblResponsable = e.Item.FindControl("lblResponsable") as Label;
                if (lblResponsable != null) lblResponsable.Text = string.Format("{0}", item.TBL_Admin_Usuarios2.Nombres);

                var imgSelectCompromiso = e.Item.FindControl("imgSelectCompromiso") as ImageButton;
                if (imgSelectCompromiso != null) imgSelectCompromiso.CommandArgument = string.Format("{0}", item.IdCompromiso);
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

        public void LoadCompromisos(List<Compromisos> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.IdFase).ThenBy(f => f.FechaCumplimiento).ToList();
            }
            rptCompromisosList.DataSource = items;
            rptCompromisosList.DataBind();
        }

        public void LoadFases(List<Fases> items)
        {
            ddlFase.DataSource = items;
            ddlFase.DataTextField = "Nombre";
            ddlFase.DataValueField = "IdFase";
            ddlFase.DataBind();
            ddlFase.Items.Insert(0, new ListItem("Ver todas las fases", "0"));            
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
      
        public int IdFase
        {
            get
            {
                return Convert.ToInt32(ddlFase.SelectedValue);
            }
            set
            {
                ddlFase.SelectedValue = value.ToString();
            }
        }

        public bool CanAddCompromiso
        {
            get
            {
                return btnAddCompromiso.Visible;
            }
            set
            {
                btnAddCompromiso.Visible = value;
            }
        }

        #endregion

        #endregion
    }
}