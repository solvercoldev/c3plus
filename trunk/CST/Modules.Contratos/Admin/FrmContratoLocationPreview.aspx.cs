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

        #endregion

        #region DropDownList

        #endregion

        #region Repeaters

        #endregion

        #endregion

        #region Methods
        
        #endregion

        #region View Members

        #region Methods

        public void LoadGmapMarkers(List<Dto_GoogleMapMarker> items)
        {
            rptMarkers.DataSource = items;
            rptMarkers.DataBind();
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
                return ViewState["AdminFasesContrato_NombreContrato"] == null ? "Administrar Fases Contrato" : ViewState["AdminFasesContrato_NombreContrato"].ToString();
            }
            set
            {
                ViewState["AdminFasesContrato_NombreContrato"] = value;
            }
        }

        public string NumeroContrato
        {
            get
            {
                return lblContrato.Text;
            }
            set
            {
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

        #endregion

        #endregion
    }
}