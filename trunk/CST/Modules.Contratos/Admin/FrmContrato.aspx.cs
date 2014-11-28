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
using System.Web.UI.HtmlControls;

namespace Modules.Contratos.Admin
{
    public partial class FrmContrato : ViewPage<ContratoPresenter, IContratoView>, IContratoView
    {
        #region Members

        public const string ROOTUC = "../UserControls/";

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

        string LastLoadedControl
        {
            get
            {
                return ViewState["LastLoaded"] as string;
            }
            set
            {
                ViewState["LastLoaded"] = value;
            }
        }

        #endregion

        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.Format("{0} - ", NombreContrato));
            ImprimirAuxTituloVentana(EstadoContrato);
            LoadUserControl();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        #endregion

        #region Menu

        protected void MnuItemClick(object sender, MenuEventArgs e)
        {
            LastLoadedControl = e.Item.Value;
            LoadUserControl();
        }

        #endregion

        #region Buttons

        protected void BtnBack_Click(object sender, EventArgs e)
        {
        }

        protected void BtnManageFases_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmManageFasesContrato.aspx?ModuleId={0}&IdContrato={1}", ModuleId, IdContrato));
        }

        protected void BtnGoToLocation_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmContratoLocationPreview.aspx?ModuleId={0}&IdContrato={1}", ModuleId, IdContrato));
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            var messages = new List<string>();

            if (messages.Any())
            {
                AddErrorMessages(messages);
                return;
            }
        }


        #endregion

        #region DropDownList

        #endregion

        #region Repeaters

        protected void RptFases_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (Fases)(e.Item.DataItem);
                // Bindind data

                var hddIdFase = e.Item.FindControl("hddIdFase") as HiddenField;
                if (hddIdFase != null) hddIdFase.Value = string.Format("{0}", item.IdFase);

                var lblPeriodo = e.Item.FindControl("lblPeriodo") as Label;
                if (lblPeriodo != null) lblPeriodo.Text = string.Format("{0}", item.Periodo);

                var lblFase = e.Item.FindControl("lblFase") as Label;
                if (lblFase != null) lblFase.Text = string.Format("{0}", item.NumeroFase);                

                var lblFechaInicio = e.Item.FindControl("lblFechaInicio") as Label;
                if (lblFechaInicio != null) lblFechaInicio.Text = string.Format("{0:dd/MM/yyyy}", item.FechaInicio);

                var lblFechaFin = e.Item.FindControl("lblFechaFin") as Label;
                if (lblFechaFin != null) lblFechaFin.Text = string.Format("{0:dd/MM/yyyy}", item.FechaFinalizacion);

                var lblDuracion = e.Item.FindControl("lblDuracion") as Label;
                if (lblDuracion != null) lblDuracion.Text = string.Format("{0}", DiffMonths(item.FechaInicio, item.FechaFinalizacion));

                var trFase = e.Item.FindControl("trFase") as HtmlTableRow;

                var imgFase = e.Item.FindControl("imgFase") as Image;
                if (imgFase != null)
                {
                    if (DateTime.Now >= item.FechaInicio && DateTime.Now <= item.FechaFinalizacion)
                    {
                        imgFase.ImageUrl = "~/Resources/images/sandglass.png";
                        item.FaseActiva = true;

                        if (trFase != null)
                        {
                            trFase.Attributes.Add("style", "background-color: #6ec138");
                        }
                    }
                    else if (DateTime.Now > item.FechaFinalizacion)
                    {
                        imgFase.ImageUrl = "~/Resources/images/check.png";
                    }
                    else if (DateTime.Now < item.FechaInicio)
                    {
                        imgFase.ImageUrl = "~/Resources/images/calendar.png";
                    }
                }
            }
        }

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

        void LoadUserControl()
        {
            var controlPath = LastLoadedControl;
            var idUc = "";

            if (string.IsNullOrEmpty(controlPath))
            {
                controlPath = "WuCAdminNovedadesContrato.ascx";
                if (mnuSecciones.Items.Count > 0)
                    mnuSecciones.Items[0].Selected = true;
            }
            if (string.IsNullOrEmpty(controlPath)) return;

            if (!controlPath.Contains('/'))
            {
                idUc = controlPath.Split('.')[0];
                controlPath = string.Format("{0}{1}", ROOTUC, controlPath);
            }
            else
            {
                idUc = "uc";
            }

            phlContent.Controls.Clear();
            var uc = LoadControl(controlPath);
            uc.ID = idUc;
            phlContent.Controls.Add(uc);
            if (controlPath.Contains("WucDocumentLibrary"))
                RegistrarControlScriptManager(uc);

            if (uc is IContratoWebUserControl)
            {
                ((IContratoWebUserControl)uc).LoadControlData();
                ((IContratoWebUserControl)uc).RiseFatherPostback += RefreshContratoInfo;
            }
        }

        public void LoadInitControl()
        {
            var controlPath = "";
            var idUc = "";

            controlPath = string.Format("{0}WuCAdminNovedadesContrato.ascx", ROOTUC);
            idUc = "WuCAdminNovedadesContrato";
        }

        void RefreshContratoInfo()
        {
            Presenter.LoadContrato();
            wucLogContrato.CargarLog();
            ImprimirAuxTituloVentana(EstadoContrato);
        }

        #endregion

        #region View Members

        #region Methods
        
        public void LoadFases(List<Fases> items)
        {
            rptFases.DataSource = items;
            rptFases.DataBind();
        }
        
        public void LoadSecciones(IEnumerable<TBL_Admin_Secciones> secciones)
        {
            mnuSecciones.Items.Clear();
            foreach (var seccione in from tab in secciones select tab)
            {
                var opcion = new MenuItem
                {
                    Text = seccione.Titulo,
                    Value =
                        (string.IsNullOrEmpty(IsEdit))
                            ? seccione.PathEdit
                            : seccione.PathPreview
                };
                mnuSecciones.Items.Add(opcion);
            }
            mnuSecciones.Items[0].Selected = true;
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

        public string EstadoContrato
        {
            get
            {
                return ViewState["AdminFasesContrato_EstadoContrato"] == null ? string.Empty : ViewState["AdminFasesContrato_EstadoContrato"].ToString();
            }
            set
            {
                ViewState["AdminFasesContrato_EstadoContrato"] = value;
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
            set
            {
                //imgImagenContrato.ImageUrl = string.Format("{0}/{1}/{2}", PathAttachedFiles, IdContrato, value);
            }
        }

        public bool CanTrabajarFases
        {
            get
            {
                return btnManageFases.Visible;
            }
            set
            {
                btnManageFases.Visible = value;
            }
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