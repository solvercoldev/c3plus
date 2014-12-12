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
    public partial class FrmNewRadicadoContrato : ViewPage<NewRadicadoContratoPresenter, INewRadicadoContratoView>, INewRadicadoContratoView
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
            ImprimirTituloVentana(string.IsNullOrEmpty(IdRadicado) ? "Nuevo Radicado - " : "Editar Radicado - ");
            ImprimirAuxTituloVentana(string.IsNullOrEmpty(IdRadicado) ? "Registro" : "Edición");
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

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            var messages = new List<string>();

            if (string.IsNullOrEmpty(Numero))
                messages.Add(string.Format("Es necesario ingresar un numero de radicado."));

            if (string.IsNullOrEmpty(Asunto))
                messages.Add(string.Format("Es necesario ingresar un asunto para el radicado."));

            if (string.IsNullOrEmpty(DirigidoAExterno) && TipoRadicado == "RS")
                messages.Add(string.Format("Es necesario ingresar un valor para el campo dirigido a."));

            if (string.IsNullOrEmpty(EnviadoPor) && TipoRadicado == "RE")
                messages.Add(string.Format("Es necesario ingresar un valor para el campo enviado por."));

            if (!fuArchivoAnexo.HasFile && string.IsNullOrEmpty(IdRadicado))
                messages.Add(string.Format("Es necesario ingresar un archivo adjunto para el radicado."));            

            if (messages.Any())
            {
                AddErrorMessages(messages);
                return;
            }

            if (string.IsNullOrEmpty(IdRadicado))
                Presenter.SaveRadicado();
            else
                Presenter.UpdateRadicado();
        }

        protected void BntArchivoRadicado_Click(object sender, EventArgs e)
        {
            DownloadDocument((byte[])ArchivoAdjunto.ComplexValue, ArchivoAdjunto.Value, "application/octet-stream");
        }

        #endregion

        #region DropDownList

        protected void TxtDiasAlarma_TextChanged(object sender, EventArgs e)
        {
            CheckAlarmaText();
        }

        void CheckAlarmaText()
        {
            lblFechaAlarma.Text = string.Format("{0:dd/MM/yyyy}", FechaRespuesta.AddDays(DiasAlarma * (-1)));
        }

        #endregion

        #region DropDownList


        #endregion

        #region RadioButton

        protected void RblTipoRadicado_IndexChanged(object sender, EventArgs e)
        {
            switch (TipoRadicado)
            {
                case "RE":
                    trRespondeRadicadoEntrada.Visible = false;
                    trRadicadoEntrada.Visible = false;
                    ddlDirigidoA.Visible = true;
                    ddlEnviadoPor.Visible = false;
                    txtDirigidoA.Visible = false;
                    txtEnviadoPor.Visible = true;
                    break;
                case "RS":
                    trRespondeRadicadoEntrada.Visible = true;;
                    RespondeRE = false;
                    trRadicadoEntrada.Visible = false;
                    ddlDirigidoA.Visible = false;
                    ddlEnviadoPor.Visible = true;
                    txtDirigidoA.Visible = true;
                    txtEnviadoPor.Visible = false;
                    break;
            }
        }

        protected void RblRespondeRE_IndexChanged(object sender, EventArgs e)
        {
            ShowRadicadoSalida(RespondeRE);
        }

        protected void RblRespuestaPendiente_IndexChanged(object sender, EventArgs e)
        {
            ShowRespuestaRadicado(RespuestaPendiente);
        }
     
        #endregion

        #endregion

        #region Methods

        public void ShowRespuestaRadicado(bool visible)
        {
            tblRespuestaRadicado.Visible = visible;
        }

        public void ShowRespondeRadicadoSalida(bool visible)
        {
            trRespondeRadicadoEntrada.Visible = visible;
            ddlDirigidoA.Visible = !visible;
            ddlEnviadoPor.Visible = visible;
            txtDirigidoA.Visible = visible;
            txtEnviadoPor.Visible = !visible;
        }

        public void ShowRadicadoSalida(bool visible)
        {
            trRadicadoEntrada.Visible = visible;
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

        public void LoadUsuarios(List<TBL_Admin_Usuarios> items)
        {
            ddlDirigidoA.DataSource = items;
            ddlDirigidoA.DataTextField = "Nombres";
            ddlDirigidoA.DataValueField = "IdUser";
            ddlDirigidoA.DataBind();

            ddlEnviadoPor.DataSource = items;
            ddlEnviadoPor.DataTextField = "Nombres";
            ddlEnviadoPor.DataValueField = "IdUser";
            ddlEnviadoPor.DataBind();

            ddlResponsableRadicado.DataSource = items;
            ddlResponsableRadicado.DataTextField = "Nombres";
            ddlResponsableRadicado.DataValueField = "IdUser";
            ddlResponsableRadicado.DataBind();
        }

        public void LoadRadicadosPendientes(List<Radicados> items)
        {
            ddlRadicadoEntrada.DataSource = items;
            ddlRadicadoEntrada.DataTextField = "Numero";
            ddlRadicadoEntrada.DataValueField = "IdRadicado";
            ddlRadicadoEntrada.DataBind();
        }

        public void GoToContratoView()
        {
            switch (FromPage)
            {
                case "contrato":
                    Response.Redirect(string.Format("FrmContrato.aspx?ModuleId={0}&IdContrato={1}", ModuleId, IdContrato));
                    break;
                case "fases":
                    Response.Redirect(string.Format("FrmManageFasesContrato.aspx?ModuleId={0}&IdContrato={1}", ModuleId, IdContrato));
                    break;
                default:
                    Response.Redirect(string.Format("FrmContrato.aspx?ModuleId={0}&IdContrato={1}", ModuleId, IdContrato));
                    break;
            }
        }

        public void GoToRadicadoView(long idRadicado)
        {
            Response.Redirect(string.Format("FrmAdminRadicadoContrato.aspx?ModuleId={0}&IdContrato={1}&IdRadicado={2}&from={2}", ModuleId, IdContrato, idRadicado, FromPage));
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
     
        public int DiasAlarma
        {
            get
            {
                return txtDiasAlarma.ValueInt;
            }
            set
            {
                txtDiasAlarma.ValueInt = value;
            }
        }

        public string TipoRadicado
        {
            get
            {
                return rblTipoRadicado.SelectedValue;
            }
            set
            {
                rblTipoRadicado.SelectedValue = value;
            }
        }

        public string Numero
        {
            get
            {
                return txtNumeroRadicado.Text;
            }
            set
            {
                txtNumeroRadicado.Text = value;
            }
        }

        public DateTime FechaRadicado
        {
            get
            {
                return Convert.ToDateTime(txtFechaRadicado.Text);
            }
            set
            {
                txtFechaRadicado.Text = value.ToString("dd/MM/yyyy");
            }
        }

        public string Asunto
        {
            get
            {
                return txtAsunto.Text;
            }
            set
            {
                txtAsunto.Text = value;
            }
        }

        public int DirigidoA
        {
            get
            {
                return Convert.ToInt32(ddlDirigidoA.SelectedValue);
            }
            set
            {
                ddlDirigidoA.SelectedValue = value.ToString();
            }
        }

        public int IdEnviadoPor
        {
            get
            {
                return Convert.ToInt32(ddlEnviadoPor.SelectedValue);
            }
            set
            {
                ddlEnviadoPor.SelectedValue = value.ToString();
            }
        }

        public string DirigidoAExterno
        {
            get
            {
                return txtDirigidoA.Text;
            }
            set
            {
                txtDirigidoA.Text = value;
            }
        }

        public string EnviadoPor
        {
            get
            {
                return txtEnviadoPor.Text;
            }
            set
            {
                txtEnviadoPor.Text = value;
            }
        }

        public string Resumen
        {
            get
            {
                return txtResumen.Text;
            }
            set
            {
                txtResumen.Text = value;
            }
        }

        public bool RespondeRE
        {
            get
            {
                return Convert.ToBoolean(rblRespondeRE.SelectedValue);
            }
            set
            {
                rblRespondeRE.SelectedValue = value.ToString();
            }
        }

        public long IdRadicadoEntradaAsociado
        {
            get
            {
                return Convert.ToInt64(ddlRadicadoEntrada.SelectedValue);
            }
            set
            {
                ddlRadicadoEntrada.SelectedValue = value.ToString();
            }
        }

        public bool RespuestaPendiente
        {
            get
            {
                return Convert.ToBoolean(rblRespuestaPendiente.SelectedValue);
            }
            set
            {
                rblRespuestaPendiente.SelectedValue = value.ToString();
            }
        }

        public string NombreAnexo
        {
            get
            {
                return fuArchivoAnexo.FileName;
            }
        }

        public byte[] ArchivoAnexo
        {
            get
            {
                return fuArchivoAnexo.FileBytes;
            }
        }

        public int ResponsableRespuesta
        {
            get
            {
                return Convert.ToInt32(ddlResponsableRadicado.SelectedValue);
            }
            set
            {
                ddlResponsableRadicado.SelectedValue = value.ToString();
            }
        }

        public DateTime FechaRespuesta
        {
            get
            {
                return Convert.ToDateTime(txtFechaRespuesta.Text);
            }
            set
            {
                txtFechaRespuesta.Text = value.ToString("dd/MM/yyyy");
                CheckAlarmaText();
            }
        }

        public DTO_ValueKey ArchivoAdjunto
        {
            get
            {
                return Session["NewRadicado_ArchivoAdjunto"] as DTO_ValueKey;
            }
            set
            {
                Session["NewRadicado_ArchivoAdjunto"] = value;
                bntArchivoRadicado.Text = value.Value;
                trInfoArchivoAnexo.Visible = true;
            }
        }
   
        #endregion

        #endregion 
    }
}