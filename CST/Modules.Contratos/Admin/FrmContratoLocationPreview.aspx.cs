using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using Application.MainModule.Contratos.DTO;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Modules.Contratos.UI;
using Presenters.Contratos.IViews;
using Presenters.Contratos.Presenters;
using System.Data;
using System.IO;

namespace Modules.Contratos.Admin
{
    public partial class FrmContratoLocationPreview : ViewPage<ContratoLocationPreviewPresenter, IContratoLocationPreviewView>, IContratoLocationPreviewView
    {
        #region Members

        public string IdContratoQS
        {
            get { return Request.QueryString.Get("IdContrato"); }
        }

        string PathAttachedFiles
        {
            get
            {
                if (ViewState["PathFiles"] == null)
                    ViewState["PathFiles"] = ConfigurationManager.AppSettings.Get("UploadFilesFolder");

                return ViewState["PathFiles"].ToString();
            }

            set { ViewState["PathFiles"] = value; }
        }

        #endregion

        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(NombreContrato);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        #endregion

        #region Menu

        #endregion

        #region Buttons

        protected void BtnBackToContrato_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmContrato.aspx?ModuleId={0}&IdContrato={1}", ModuleId, IdContrato));
        }

        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            EnableEdit(true);
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            Presenter.SaveContrato();
        }

        #endregion

        #region DropDownList

        #endregion

        #region Repeaters

        #endregion

        #endregion

        #region Methods

        public void EnableEdit(bool enable)
        {
            lblContrato.Visible = !enable;
            txtNumeroContrato.Visible = enable;

            lblEmpresa.Visible = !enable;
            ddlEmpresa.Visible = enable;

            lblBloque.Visible = !enable;
            ddlBloque.Visible = enable;

            lblTipoContrato.Visible = !enable;
            ddlTipoContrato.Visible = enable;

            trUbicacion.Visible = enable;
            trArchivoSitio.Visible = enable;

            btnEditar.Visible = !enable;
            btnSave.Visible = enable;
        }

        public void CanEdit(bool enable)
        {
            btnEditar.Visible = enable;
        }
        
        #endregion

        #region View Members

        #region Methods

        public void LoadGmapMarkers(List<Dto_GoogleMapMarker> items)
        {
            rptMarkers.DataSource = items;
            rptMarkers.DataBind();
        }

        public void LoadEmpresas(List<Empresas> items)
        {
            ddlEmpresa.DataSource = items;
            ddlEmpresa.DataTextField = "RazonSocial";
            ddlEmpresa.DataValueField = "Nit";
            ddlEmpresa.DataBind();
        }

        public void LoadTipoContratos(List<TiposContrato> items)
        {
            ddlTipoContrato.DataSource = items;
            ddlTipoContrato.DataTextField = "Descripcion";
            ddlTipoContrato.DataValueField = "IdTipoContrato";
            ddlTipoContrato.DataBind();
        }

        public void LoadBloques(DataTable items)
        {
            ddlBloque.DataSource = items;
            ddlBloque.DataValueField = "IdBloque";
            ddlBloque.DataTextField = "Descripcion";
            ddlBloque.DataBind();
        }

        public void SaveImagenContrato(int idContrato)
        {
            if (fuImagenContrato.HasFile)
            {
                var uploadFolder = Server.MapPath(PathAttachedFiles);
                uploadFolder = string.Format("{0}/{1}", uploadFolder, idContrato);
                if (!Directory.Exists(uploadFolder))
                    Directory.CreateDirectory(uploadFolder);

                fuImagenContrato.SaveAs(string.Format("{0}/{1}", uploadFolder, fuImagenContrato.FileName));
            }
        }

        public void AddErrorMessages(List<string> messages)
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
            get
            {
                return ViewState["AdminFasesContrato_IdContrato"] == null ? IdContratoQS : ViewState["AdminFasesContrato_IdContrato"].ToString();
            }
            set
            {
                ViewState["AdminFasesContrato_IdContrato"] = value;
            }
        }

        public string Empresa
        {
            get
            {
                return lblEmpresa.Text;
            }
            set
            {
                lblEmpresa.Text = value;
            }
        }

        public string Bloque
        {
            get
            {
                return lblBloque.Text;
            }
            set
            {
                lblBloque.Text = value;
            }
        }

        public string FechaFirma
        {
            get
            {
                return lblFechaFirma.Text;
            }
            set
            {
                lblFechaFirma.Text = UppercaseFirst(value);
            }
        }

        public string FechaEfectiva
        {
            get
            {
                return lblFechaEfectiva.Text;
            }
            set
            {
                lblFechaEfectiva.Text = UppercaseFirst(value);
            }
        }

        public string Periodo
        {
            get
            {
                return lblPeriodo.Text;
            }
            set
            {
                lblPeriodo.Text = value;
            }
        }

        public string NombreContrato
        {
            get
            {
                return ViewState["AdminContrato_NombreContrato"] == null ? "Preview Contrato" : ViewState["AdminContrato_NombreContrato"].ToString();
            }
            set
            {
                ViewState["AdminContrato_NombreContrato"] = value;
            }
        }

        public string EstadoContrato
        {
            get
            {
                return ViewState["AdminContrato_EstadoContrato"] == null ? string.Empty : ViewState["AdminContrato_EstadoContrato"].ToString();
            }
            set
            {
                ViewState["AdminContrato_EstadoContrato"] = value;
                ImprimirAuxTituloVentana(value);
            }
        }

        public string NumeroContrato
        {
            get
            {
                return txtNumeroContrato.Text;
            }
            set
            {
                txtNumeroContrato.Text = value;
                lblContrato.Text = value;
            }
        }

        public string ImagenContrato
        {
            set { imgImagenContrato.ImageUrl = string.Format("{0}/{1}/{2}", PathAttachedFiles, IdContrato, value); }
        }

        public string TipoContrato
        {
            get
            {
                return lblTipoContrato.Text;
            }
            set
            {
                lblTipoContrato.Text = value;
            }
        }

        public string IdEmpresa
        {
            get
            {
                return ddlEmpresa.SelectedValue;
            }
            set
            {
                ddlEmpresa.SelectedValue = value;
            }
        }

        public string IdTipoContrato
        {
            get
            {
                return ddlTipoContrato.SelectedValue;
            }
            set
            {
                ddlTipoContrato.SelectedValue = value;
            }
        }

        public string IdBloque
        {
            get
            {
                return ddlBloque.SelectedValue;
            }
            set
            {
                ddlBloque.SelectedValue = value;
            }
        }

        public string ImagenContratoEdit
        {
            get
            {
                return fuImagenContrato.HasFile ? fuImagenContrato.FileName : string.Empty;
            }
        }


        public decimal? Longitud
        {
            get
            {
                if (txtLongitud.Value != null)
                    return txtLongitud.ValueDecimal;
                else
                    return null;
            }
            set
            {
                txtLongitud.Value = value;
            }
        }

        public decimal? Latitud
        {
            get
            {
                if (txtLatitud.Value != null)
                    return txtLatitud.ValueDecimal;
                else
                    return null;
            }
            set
            {
                txtLatitud.Value = value;
            }
        }

        #endregion

        #endregion
    }
}