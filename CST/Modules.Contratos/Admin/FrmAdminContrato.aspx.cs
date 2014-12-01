using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Contratos.IViews;
using Presenters.Contratos.Presenters;
using System.Data;

namespace Modules.Contratos.Admin
{
    public partial class FrmAdminContrato : ViewPage<AdminContratoPresenter, IAdminContratoView>, IAdminContratoView
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
            if (string.IsNullOrEmpty(IdContrato))
                ImprimirTituloVentana(string.Format("Nuevo Contrato"));
            else
                ImprimirTituloVentana(string.Format("Editar Contrato"));
        }        

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        #endregion

        #region Buttons

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            //GoToContratoList();
            Presenter.CancelContrato();
        }


        protected void BtnSave_Click(object sender, EventArgs e)
        {
            var messages = new List<string>();

            if (string.IsNullOrEmpty(NumeroContrato))
                messages.Add("Es necesario ingresar el numero del contrato.");

            if (string.IsNullOrEmpty(Nombre))
                messages.Add("Es necesario ingresar el nombre del contrato.");

            if (string.IsNullOrEmpty(Descripcion))
                messages.Add("Es necesario ingresar una descripción del contrato.");

            if (fuImagenContrato.HasFile)
            {
                // Verificando Imagenes a cargar
                if (fuImagenContrato.PostedFile.ContentLength > 4194304)
                    messages.Add("El tamaño de la imagen no debe exceder los 4 MB");

                var supportedTypes = new[] { "jpg", "jpeg", "png" };

                var fileExt = System.IO.Path.GetExtension(fuImagenContrato.FileName).Substring(1);

                if (!supportedTypes.Contains(fileExt))
                    messages.Add("Tipo de imagen invalido. Solo se soportan los siguientes tipos: jpg, jpeg y png.");
            }            

            if (messages.Any())
            {
                AddErrorMessages(messages);
                return;
            }

            if (string.IsNullOrEmpty(IdContratoQS))
                Presenter.SaveContrato();
            else
                Presenter.UpdateContrato();
        }

        #endregion

        #region TextBox

        protected void FechaFirma_TextChanged(object sender, EventArgs e)
        {
            cexTxtfechaEfectiva.StartDate = FechaFirma.AddDays(1);
            FechaEfectiva = FechaFirma.AddDays(1);
        }


        #endregion

        #region Repeaters

        #endregion

        #endregion

        #region Methods

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

        public void LoadResponsables(List<TBL_Admin_Usuarios> items)
        {
            ddlResponsable.DataSource = items;
            ddlResponsable.DataValueField = "IdUser";
            ddlResponsable.DataTextField = "Nombres";
            ddlResponsable.DataBind();
        }

        public void GoToAdminFases(int idContrato)
        {
            Response.Redirect(string.Format("FrmAdminFasesContrato.aspx?ModuleId={0}&IdContrato={1}", ModuleId, idContrato));
        }

        public void GoToContratoList()
        {
            Response.Redirect(string.Format("../Views/GeneralContractList.aspx?ModuleId={0}", ModuleId));
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

        #endregion

        #endregion

        public string IdContrato
        {
            get
            {
                return ViewState["AdminContrato_IdContrato"] == null ? IdContratoQS : ViewState["AdminContrato_IdContrato"].ToString();
            }
            set
            {
                ViewState["AdminContrato_IdContrato"] = value;
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
            }
        }

        public string Nombre
        {
            get
            {
                return txtNombreContrato.Text;
            }
            set
            {
                txtNombreContrato.Text = value;
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

        public DateTime FechaFirma
        {
            get
            {
                return Convert.ToDateTime(txtFechaFirma.Text);
            }
            set
            {
                txtFechaFirma.Text = value.ToString("dd/MM/yyyy");
            }
        }

        public DateTime FechaEfectiva
        {
            get
            {
                return Convert.ToDateTime(txtFechaEfectiva.Text);
            }
            set
            {
                txtFechaEfectiva.Text = value.ToString("dd/MM/yyyy");
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

        public int IdResponsable
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

        public int IdEstado
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string ImagenContrato
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
    }
}