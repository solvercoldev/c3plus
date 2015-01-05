using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Application.Core;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Contratos.IViews;
using Presenters.Contratos.Presenters;

namespace Modules.Contratos.Admin
{
    public partial class FrmAdminCompromisoContrato : ViewPage<AdminCompromisoContratoPresenter, IAdminCompromisoContratoNew>, IAdminCompromisoContratoNew
    {
        #region Members

        public string FromPage
        {
            get
            {
                return Request.QueryString["from"];
            }
        }

        public string FromPageAux
        {
            get
            {
                return Request.QueryString["fromaux"];
            }
        }

        public string IdFrom
        {
            get
            {
                return Request.QueryString["idfrom"];
            }
        }

        public string IdFromAux
        {
            get
            {
                return Request.QueryString["idfromaux"];
            }
        }

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

        #region Menu

        #endregion

        #region Buttons

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            GoToContratoView();
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            EnableEditCompromiso(true);
        }

        protected void BtnSendNotify_Click(object sender, EventArgs e)
        {
            Presenter.SendNotifyMail();
            ShowMessageOk("Notificación Enviada a Responsable");
        }

        protected void BtnSalir_Click(object sender, EventArgs e)
        {
            EnableEditCompromiso(false);
        }

        protected void BtnAddNovedad_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var cmd = btn.CommandArgument;
            TipoOperacion = cmd;
            switch (cmd)
            {
                case "Anular":
                    trFechaProgramacion.Visible = false;
                    trResponsable.Visible = false;
                    break;
                case "Confirmar":
                    trFechaProgramacion.Visible = false;
                    trResponsable.Visible = false;
                    break;
                case "Reprogramar":
                    trFechaProgramacion.Visible = true;
                    trResponsable.Visible = false;
                    break;
                case "ReAsignar":
                    trFechaProgramacion.Visible = false;
                    trResponsable.Visible = true;
                    break;
            }

            txtObservacionesNovedad.Text = string.Empty;

            ShowNovedadWindow(true);
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            var messages = new List<string>();

            if (messages.Any())
            {
                AddErrorMessages(messages);
                return;
            }

            Presenter.SaveCompromiso();
        }

        protected void BtnSaveNovedad_Click(object sender, EventArgs e)
        {
            Presenter.AddNovedadCompromiso();
        }

        #endregion

        #region DropDownList

        #endregion

        #region RadioButton

        #endregion

        #endregion

        #region Methods

        public void EnableEditCompromiso(bool enable)
        {
            txtDescripcion.Enabled = enable;

            ddlTipoPago.Visible = enable;
            lblTipoPago.Visible = !enable;

            ddlEntidad.Visible = enable;
            lblEntidad.Visible = !enable;

            txtValorCoberturaPago.Visible = enable;
            lblValorCobertura.Visible = !enable;

            txtPagoNumeroDocumento.Visible = enable;
            lblNumeroDocumento.Visible = !enable;

            txtValorPago.Visible = enable;
            lblValor.Visible = !enable;

            ddlMoneda.Visible = enable;
            ddlMonedaCobertura.Visible = enable;

            btnSave.Visible = enable;

            btnSendNotify.Visible = !enable;

            btnEdit.Visible = !enable;
            btnAnular.Visible = !enable;
            btnMarcarOk.Visible = !enable;
            btnReprogramar.Visible = !enable;
            btnReasignar.Visible = !enable;
        }

        void AddErrorMessages(List<string> messages)
        {
            if (messages.Any())
            {
                foreach (var msg in messages)
                {
                    var custVal = new CustomValidator();
                    custVal.IsValid = false;
                    custVal.ErrorMessage = msg;
                    custVal.EnableClientScript = false;
                    custVal.Display = ValidatorDisplay.None;
                    custVal.ValidationGroup = "vgGeneral";
                    this.Page.Form.Controls.Add(custVal);
                }
            }
        }

        #endregion

        #region View Members

        #region Methods

        public void LoadManuales(List<DTO_ValueKey> items)
        {
            lstEntregablesANH.DataSource = items;
            lstEntregablesANH.DataTextField = "Value";
            lstEntregablesANH.DataValueField = "Id";
            lstEntregablesANH.DataBind();
        }

        public void LoadMonedas(List<Monedas> items)
        {
            ddlMoneda.DataSource = items;
            ddlMoneda.DataTextField = "Nombre";
            ddlMoneda.DataValueField = "IdMoneda";
            ddlMoneda.DataBind();

            ddlMonedaCobertura.DataSource = items;
            ddlMonedaCobertura.DataTextField = "Nombre";
            ddlMonedaCobertura.DataValueField = "IdMoneda";
            ddlMonedaCobertura.DataBind();
        }

        public void LoadTipoPago(List<TiposPagoObligacion> items)
        {
            ddlTipoPago.DataSource = items;
            ddlTipoPago.DataTextField = "Descripcion";
            ddlTipoPago.DataValueField = "IdTipoPagoObligacion";
            ddlTipoPago.DataBind();
        }

        public void LoadEntidades(List<Terceros> items)
        {
            ddlEntidad.DataSource = items;
            ddlEntidad.DataTextField = "Nombre";
            ddlEntidad.DataValueField = "IdTercero";
            ddlEntidad.DataBind();
        }

        public void LoadResponsables(List<TBL_Admin_Usuarios> items)
        {
            ddlResponsable.DataSource = items;
            ddlResponsable.DataTextField = "Nombres";
            ddlResponsable.DataValueField = "IdUser";
            ddlResponsable.DataBind();
        }

        public void ShowTipoAsociacion(string tipo)
        {
            switch (tipo)
            {
                case "No":
                    tblPagosObligaciones.Visible = false;
                    tblEntregablesANH.Visible = false;
                    break;

                case "Entregable":
                    tblPagosObligaciones.Visible = false;
                    tblEntregablesANH.Visible = true;
                    break;

                case "Pago":

                    tblPagosObligaciones.Visible = true;
                    tblEntregablesANH.Visible = false;

                    break;
            }
        }

        public void ShowNovedadWindow(bool visible)
        {
            if (visible)
                mpeAdminNovedad.Show();
            else
                mpeAdminNovedad.Hide();
        }

        public void EnableActions(bool enable)
        {
            divActionButtons.Visible = enable;
        }
        
        public void GoToContratoView()
        {
            switch (FromPage)
            {
                case "vcompromisos":
                    Response.Redirect(string.Format("../Views/FrmTotalCompromisos.aspx?ModuleId={0}", ModuleId));
                    break;
                case "miscompendiente":
                    Response.Redirect(string.Format("../Views/MisCompromisosPendientes.aspx?ModuleId={0}", ModuleId));
                    break;
                case "fases":
                    Response.Redirect(string.Format("FrmManageFasesContrato.aspx?ModuleId={0}&IdContrato={1}", ModuleId, IdContrato));
                    break;
                default:
                    Response.Redirect(string.Format("FrmContrato.aspx?ModuleId={0}&IdContrato={1}", ModuleId, IdContrato));
                    break;
            }
        }

        #endregion

        #region Properties

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

        public string IdCompromiso
        {
            get { return Request.QueryString.Get("IdCompromiso"); }
        }

        public string Nombre
        {
            get
            {
                return string.Format("{0}", ViewState["AdminCompromiso_Nombre"]);
            }
            set
            {
                ViewState["AdminCompromiso_Nombre"] = value;
                ImprimirTituloVentana(value);                
            }
        }

        public string Estado
        {
            get
            {
                return string.Format("{0}", ViewState["AdminCompromiso_Estado"]);
            }
            set
            {
                ViewState["AdminCompromiso_Estado"] = value;
                ImprimirAuxTituloVentana(value);
            }
        }

        public string Descripcion
        {
            get
            {
                return txtDescripcion.Text;
            }
            set
            {
                txtDescripcion.Text = value;
            }
        }

        public DateTime FechaCumplimiento
        {
            get
            {
                return Convert.ToDateTime(ViewState["AdminCompromiso_FechaCumplimiento"]);
            }
            set
            {
                ViewState["AdminCompromiso_FechaCumplimiento"] = value;
            }
        }

        public string FechaCumplimientoStr
        {
            get
            {
                return lblFechaCompromiso.Text;
            }
            set
            {
                lblFechaCompromiso.Text = value;
            }
        }

        public string Responsable
        {
            get
            {
                return lblResponsable.Text;
            }
            set
            {
                lblResponsable.Text = value;
            }
        }

        public string TipoCompromiso
        {
            get
            {
                return lblTipoCompromiso.Text;
            }
            set
            {
                lblTipoCompromiso.Text = value;
            }
        }

        public string Importancia
        {
            get
            {
                return lblImportancia.Text;
            }
            set
            {
                lblImportancia.Text = value;
            }
        }

        public string PeridoFase
        {
            get
            {
                return lblTipoFase.Text;
            }
            set
            {
                lblTipoFase.Text = value;
            }
        }

        public string Fase
        {
            get
            {
                return lblFase.Text;
            }
            set
            {
                lblFase.Text = value;
            }
        }

        public string BCP
        {
            get
            {
                return lblBCP.Text;
            }
            set
            {
                lblBCP.Text = value;
            }
        }

        public string ResponsableReprogramacion
        {
            get
            {
                return ddlResponsable.SelectedItem.Text;
            }
            set
            {
                ddlResponsable.SelectedValue = value;
            }
        }

        public int IdResponsableReprogramacion
        {
            get
            {
                return Convert.ToInt32(ddlResponsable.SelectedValue);
            }
            set
            {
                ddlResponsable.SelectedValue = value.ToString();
            }
        }

        public string TipoPago
        {
            get
            {
                return lblTipoPago.Text;
            }
            set
            {
                lblTipoPago.Text = value;
            }
        }

        public string Entidad
        {
            get
            {
                return lblEntidad.Text;
            }
            set
            {
                lblEntidad.Text = value;
            }
        }

        public string NumeroDocumento
        {
            get
            {
                return txtPagoNumeroDocumento.Text;
            }
            set
            {
                txtPagoNumeroDocumento.Text = value;
                lblNumeroDocumento.Text = value;
            }
        }

        public string Valor
        {
            get
            {
                return lblValor.Text;
            }
            set
            {
                lblValor.Text = value;
            }
        }

        public string ValorCobertura
        {
            get
            {
                return lblValorCobertura.Text;
            }
            set
            {
                lblValorCobertura.Text = value;
            }
        }

        public string IdTercero
        {
            get
            {
                return ddlEntidad.SelectedValue;
            }
            set
            {
                ddlEntidad.SelectedValue = value;
            }
        }

        public string IdTipoPagoObligacion
        {
            get
            {
                return ddlTipoPago.SelectedValue;
            }
            set
            {
                ddlTipoPago.SelectedValue = value;
            }
        }

        public string IdMoneda
        {
            get
            {
                return ddlMoneda.SelectedValue;
            }
            set
            {
                ddlMoneda.SelectedValue = value;
            }
        }

        public string IdMonedaCobertura
        {
            get
            {
                return ddlMonedaCobertura.SelectedValue;
            }
            set
            {
                ddlMonedaCobertura.SelectedValue = value;
            }
        }

        public decimal ValorPago
        {
            get
            {
                return txtValorPago.ValueDecimal;
            }
            set
            {
                txtValorPago.ValueDecimal = value;
            }
        }

        public decimal ValorCoberturaPago
        {
            get
            {
                return txtValorCoberturaPago.ValueDecimal;
            }
            set
            {
                txtValorCoberturaPago.ValueDecimal = value;
            }
        }

        public DateTime FechaReprogramacion
        {
            get
            {
                return Convert.ToDateTime(txtFechaReProgramacion.Text);
            }
            set
            {
                txtFechaReProgramacion.Text = value.ToString("dd/MM/yyyy");
            }
        }

        public string TipoOperacion
        {
            get
            {
                return string.Format("{0}", ViewState["AdminCompromiso_TipoOperacion"]);
            }
            set
            {
                ViewState["AdminCompromiso_TipoOperacion"] = value;
            }
        }

        public string MsgLogInfo
        {
            get
            {
                return lblMsgLogInfo.Text;
            }
            set
            {
                lblMsgLogInfo.Text = value;
            }
        }

        public string ObservacionesNovedad
        {
            get
            {
                return txtObservacionesNovedad.Text;
            }
            set
            {
                txtObservacionesNovedad.Text = value;
            }
        }

        public string InfoContrato
        {
            get
            {
                return lblInfoContrato.Text;
            }
            set
            {
                lblInfoContrato.Text = value;
                lblInfoContrato.NavigateUrl = string.Format("FrmContrato.aspx?ModuleId={0}&IdContrato={1}", ModuleId, IdContrato);
            }
        }

        #endregion

        #endregion                  
    }
}