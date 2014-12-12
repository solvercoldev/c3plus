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
    public partial class FrmAdminRadicadoContrato : ViewPage<AdminRadicadoContratoPresenter, IAdminRadicadoContratoView>, IAdminRadicadoContratoView
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
            Response.Redirect(string.Format("FrmNewRadicadoContrato.aspx?ModuleId={0}&IdContrato={1}&from={2}&IdRadicado={3}", ModuleId, IdContrato, FromPage, IdRadicado));
        }

        protected void BtnSalir_Click(object sender, EventArgs e)
        {
            EnableEdit(false);
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

            Presenter.SaveRadicado();
        }

        protected void BtnSaveNovedad_Click(object sender, EventArgs e)
        {
            Presenter.AddNovedadRadicado();
        }

        protected void BntArchivoRadicado_Click(object sender, EventArgs e)
        {
            DownloadDocument((byte[])ArchivoAdjunto.ComplexValue, ArchivoAdjunto.Value, "application/octet-stream");
        }

        #endregion

        #region DropDownList

        #endregion

        #region RadioButton

        #endregion

        #endregion

        #region Methods

        public void EnableEdit(bool enable)
        {
            //txtDescripcion.Visible = enable;
            //lblDescripcion.Visible = !enable;

            //ddlTipoPago.Visible = enable;
            //lblTipoPago.Visible = !enable;

            //ddlEntidad.Visible = enable;
            //lblEntidad.Visible = !enable;

            //txtValorCoberturaPago.Visible = enable;
            //lblValorCobertura.Visible = !enable;

            //txtPagoNumeroDocumento.Visible = enable;
            //lblNumeroDocumento.Visible = !enable;

            //txtValorPago.Visible = enable;
            //lblValor.Visible = !enable;

            //ddlMoneda.Visible = enable;

            btnSave.Visible = enable;

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

        public void LoadResponsables(List<TBL_Admin_Usuarios> items)
        {
            ddlResponsable.DataSource = items;
            ddlResponsable.DataTextField = "Nombres";
            ddlResponsable.DataValueField = "Nombres";
            ddlResponsable.DataBind();
        }       

        public void ShowNovedadWindow(bool visible)
        {
            if (visible)
                mpeAdminNovedad.Show();
            else
                mpeAdminNovedad.Hide();
        }

        public void ShowRespuesta(bool visible)
        {
            tblRespuestaRadicado.Visible = visible;
        }

        public void ShowREAsociado(bool visible)
        {
            trREAsociado.Visible = visible;
        }

        public void EnableActions(bool enable)
        {
            divActionButtons.Visible = enable;
        }

        public void EnableMarcarOK(bool enable)
        {
            btnMarcarOk.Visible = enable;
        }

        public void EnableAnular(bool enable)
        {
            btnAnular.Visible = enable;
        }

        public void EnableReprogramar(bool enable)
        {
            btnReprogramar.Visible = enable;
        }

        public void EnableReasignar(bool enable)
        {
            btnReasignar.Visible = enable;
        }


        public void GoToContratoView()
        {
            switch (FromPage)
            {
                case "vcompromisos":
                    Response.Redirect(string.Format("../Views/FrmTotalCompromisos.aspx?ModuleId={0}", ModuleId));
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

        public string IdRadicado
        {
            get { return Request.QueryString.Get("IdRadicado"); }
        }

        public string Numero
        {
            get
            {
                return string.Format("{0}", ViewState["AdminRadicado_Numero"]);
            }
            set
            {
                ViewState["AdminRadicado_Numero"] = value;
                ImprimirTituloVentana(value);
            }
        }
       
        public string Estado
        {
            get
            {
                return string.Format("{0}", ViewState["AdminRadicado_Estado"]);
            }
            set
            {
                ViewState["AdminRadicado_Estado"] = value;
                ImprimirAuxTituloVentana(value);
            }
        }

        public string ResponsableReprogramacion
        {
            get
            {
                return ddlResponsable.SelectedValue;
            }
            set
            {
                ddlResponsable.SelectedValue = value;
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
                return string.Format("{0}", ViewState["AdminRadicado_TipoOperacion"]);
            }
            set
            {
                ViewState["AdminRadicado_TipoOperacion"] = value;
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

        public string FechaVencimiento
        {
            get
            {
                return lblFechaRadicado.Text;
            }
            set
            {
                lblFechaRadicado.Text = value;
            }
        }

        public string FechaCreacion
        {
            get
            {
                return lblFechaCreacion.Text;
            }
            set
            {
                lblFechaCreacion.Text = value;
            }
        }

        public string Asunto
        {
            get
            {
                return lblAsunto.Text;
            }
            set
            {
                lblAsunto.Text = value;
            }
        }

        public string EnviadoPor
        {
            get
            {
                return lblEnviadoPor.Text;
            }
            set
            {
                lblEnviadoPor.Text = value;
            }
        }

        public string DirigidoA
        {
            get
            {
                return lblDirigidoA.Text;
            }
            set
            {
                lblDirigidoA.Text = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return lblDescripcion.Text;
            }
            set
            {
                lblDescripcion.Text = value;
            }
        }

        public string REAsociado
        {
            get
            {
                return lblRadicadoAsociado.Text;
            }
            set
            {
                lblRadicadoAsociado.Text = value;
            }
        }

        public DTO_ValueKey ArchivoAdjunto
        {
            get
            {
                return Session["AdminRadicado_ArchivoAdjunto"] as DTO_ValueKey;
            }
            set
            {
                Session["AdminRadicado_ArchivoAdjunto"] = value;
                bntArchivoRadicado.Text = value.Value;
            }
        }

        public string ResponsableRespuesta
        {
            get
            {
                return lblResponsableRespuesta.Text;
            }
            set
            {
                lblResponsableRespuesta.Text = value;
                lblResponsable.Text = value;
            }
        }

        public string FechaRespuesta
        {
            get
            {
                return lblFechaRespuesta.Text;
            }
            set
            {
                lblFechaRespuesta.Text = value;
            }
        }

        public string FechaAlarmaRespuesta
        {
            get
            {
                return lblFechaAlarma.Text;
            }
            set
            {
                lblFechaAlarma.Text = value;
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