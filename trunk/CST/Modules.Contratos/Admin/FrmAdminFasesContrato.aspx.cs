using System;
using System.Web.UI;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Contratos.IViews;
using Presenters.Contratos.Presenters;
using Application.Core;
using Application.MainModule.Contratos.DTO;
using Infragistics.Web.UI.EditorControls;

namespace Modules.Contratos.Admin
{
    public partial class FrmAdminFasesContrato : ViewPage<AdminFasesContratoPresenter, IAdminFasesContratoView>, IAdminFasesContratoView
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
            ImprimirTituloVentana(NombreContrato);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        #endregion

        #region Buttons

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmAdminContrato.aspx?ModuleId={0}&IdContrato={1}", ModuleId, IdContratoQS));
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            var messages = new List<string>();

            if (messages.Any())
            {
                AddErrorMessages(messages);
                return;
            }

            Presenter.SaveFases();
        }

        protected void BtnGenerarFases_Click(object sender, EventArgs e)
        {
            var auxDate = FechaEfectivaInit;
            FasesContrato = new List<Dto_FaseContrato>();
            if (TieneFase0)
            {
                FasesContrato.Add(new Dto_FaseContrato()
                                        {
                                            FaseId = 0,
                                            Periodo = "Fase Cero",
                                            Fase = string.Format("Fase {0}", 0),
                                            DuracionFase = MinDuracionFaseCero,
                                            FechaInicio = FechaFirmaInit.AddDays(1),
                                            FechaFin = auxDate.AddMonths(MinDuracionFaseCero).AddDays(-1),
                                            MinDuracionFase = MinDuracionFaseCero,
                                            MaxDuracionFase = MaxDuracionFaseCero,
                                            Grupo = 0
                                        }
                                );
            }                

            for (int i = 1; i <= NumeroFases; i++)
            {                
                var fase = new Dto_FaseContrato();
                fase.FaseId = i;
                fase.Periodo = "Exploratorio";
                fase.Fase = string.Format("Fase {0}", i);
                fase.FechaInicio = auxDate;
                fase.FechaFin = auxDate.AddMonths(MinDuracionFaseExploratorio).AddDays(-1);
                fase.DuracionFase = MinDuracionFaseExploratorio;
                fase.MinDuracionFase = MinDuracionFaseExploratorio;
                fase.MaxDuracionFase = MaxDuracionFaseExploratorio;
                fase.Grupo = 0;

                if (i == NumeroFases)
                {
                    fase.FechaFin = auxDate.AddMonths(MinDuracionFaseExploratorio);
                }

                FasesContrato.Add(fase);

                auxDate = auxDate.AddMonths(MinDuracionFaseExploratorio);
            }

            LoadFasesContrato();
        }

        #endregion

        #region TextBoxes

        protected void TxtDuracionFases_TextChanged(object sender, EventArgs e)
        {
            var txtDuracion = (WebNumericEditor)sender;

            var id = Convert.ToInt32(txtDuracion.ToolTip);
            
            var fase = FasesContrato.Where(x => x.FaseId == id).First();
            fase.DuracionFase = txtDuracion.ValueInt;
            var auxDate = fase.FechaInicio.AddMonths(fase.DuracionFase);
            fase.FechaFin = auxDate;
            
            if (id != 0)
            {
                id++;
                while (id < FasesContrato.Count)
                {
                    fase = FasesContrato.Where(x => x.FaseId == id).First();
                    fase.FechaInicio = auxDate;
                    auxDate = fase.FechaInicio.AddMonths(fase.DuracionFase);
                    fase.FechaFin = auxDate.AddDays(-1);

                    id++;
                }

                fase.FechaFin = auxDate;
            }                        

            LoadFasesContrato();
        }

        #endregion

        #region TextBox

        protected void FechaFinalFase_ValueChanged(object sender, EventArgs e)
        {
            var tbFechaFinal = (TextBox)sender;

            var id = Convert.ToInt32(tbFechaFinal.ToolTip);

            var fase = FasesContrato.Where(x => x.FaseId == id).First();
            fase.FechaFin = Convert.ToDateTime(tbFechaFinal.Text);
            var auxDate = fase.FechaFin;
            fase.DuracionFase = DiffMonths(fase.FechaInicio, fase.FechaFin);

            if (id != 0)
            {
                id++;
                while (id < FasesContrato.Count)
                {
                    fase = FasesContrato.Where(x => x.FaseId == id).First();
                    fase.FechaInicio = auxDate;
                    auxDate = fase.FechaInicio.AddMonths(fase.DuracionFase);
                    fase.FechaFin = auxDate.AddDays(-1);

                    id++;
                }
                fase.FechaFin = auxDate;
            }

            LoadFasesContrato();
        }

        #endregion

        #region Repeaters

        protected void RptFases_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (Dto_FaseContrato)(e.Item.DataItem);
                // Bindind data

                var hddIdFase = e.Item.FindControl("hddIdFase") as HiddenField;
                if (hddIdFase != null) hddIdFase.Value = string.Format("{0}", item.FaseId);

                var lblPeriodo = e.Item.FindControl("lblPeriodo") as Label;
                if (lblPeriodo != null) lblPeriodo.Text = string.Format("{0}", item.Periodo);

                var lblFase = e.Item.FindControl("lblFase") as Label;
                if (lblFase != null) lblFase.Text = string.Format("{0}", item.FaseId);

                var txtDuracionFases = e.Item.FindControl("txtDuracionFases") as WebNumericEditor;
                if (txtDuracionFases != null)
                {
                    txtDuracionFases.MinValue = item.MinDuracionFase;
                    txtDuracionFases.MaxValue = item.MaxDuracionFase;

                    txtDuracionFases.ToolTip = string.Format("{0}", item.FaseId);

                    txtDuracionFases.Value = item.DuracionFase;
                }

                var lblFechaInicio = e.Item.FindControl("lblFechaInicio") as Label;
                if (lblFechaInicio != null) lblFechaInicio.Text = string.Format("{0:dd/MM/yyyy}", item.FechaInicio);

                var txtFechaFin = e.Item.FindControl("txtFechaFin") as TextBox;
                if (txtFechaFin != null)
                {
                    txtFechaFin.Text = string.Format("{0:dd/MM/yyyy}", item.FechaFin);
                    txtFechaFin.ToolTip = string.Format("{0}", item.FaseId);
                }
            }
        }

        #endregion

        #endregion

        #region Methods

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

        void LoadFasesContrato()
        {
            rptFases.DataSource = FasesContrato;
            rptFases.DataBind();

            SetPeriodo();
        }

        void SetPeriodo()
        {
            var primeraFase = FasesContrato[0];
            var ultimaFase = FasesContrato[FasesContrato.Count - 1];

            Periodo = string.Format("{0}", UppercaseFirst(string.Format("{0:MMMM} {0:dd} de {0:yyyy}", ultimaFase.FechaFin)));
        }

        #endregion

        #region View Members

        #region Methods

        public void GoToAdminContrato()
        {
            Response.Redirect(string.Format("FrmContrato.aspx?ModuleId={0}&IdContrato={1}", ModuleId, IdContrato));
        }

        public void GoToContratoList()
        {
            Response.Redirect(string.Format("../Views/GeneralContractList.aspx?ModuleId={0}", ModuleId));
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
                lblPeriodo.Text = UppercaseFirst(value);
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

        public List<DTO_ValueKey> TiposFase
        {
            get
            {
                if (Session["AdminFasesContrato_TiposFase"] == null)
                    Session["AdminFasesContrato_TiposFase"] = new List<DTO_ValueKey>();

                return Session["AdminFasesContrato_TiposFase"] as List<DTO_ValueKey>;
            }
            set
            {
                Session["AdminFasesContrato_TiposFase"] = value;
            }
        }

        public List<DTO_ValueKey> DuracionContrato
        {
            get
            {
                if (Session["AdminFasesContrato_DuracionContrato"] == null)
                    Session["AdminFasesContrato_DuracionContrato"] = new List<DTO_ValueKey>();

                return Session["AdminFasesContrato_DuracionContrato"] as List<DTO_ValueKey>;
            }
            set
            {
                Session["AdminFasesContrato_DuracionContrato"] = value;
            }
        }

        public List<Dto_FaseContrato> FasesContrato
        {
            get
            {
                if (Session["AdminFasesContrato_FasesContrato"] == null)
                    Session["AdminFasesContrato_FasesContrato"] = new List<Dto_FaseContrato>();

                return Session["AdminFasesContrato_FasesContrato"] as List<Dto_FaseContrato>;
            }
            set
            {
                Session["AdminFasesContrato_FasesContrato"] = value;
            }
        }

        public DateTime FechaFirmaInit
        {
            get
            {
                if (Session["AdminFasesContrato_FechaFirmaInit"] == null)
                    Session["AdminFasesContrato_FechaFirmaInit"] = DateTime.Now;

                return Convert.ToDateTime(Session["AdminFasesContrato_FechaFirmaInit"]);
            }
            set
            {
                Session["AdminFasesContrato_FechaFirmaInit"] = value;
            }
        }

        public DateTime FechaEfectivaInit
        {
            get
            {
                if (Session["AdminFasesContrato_FechaEfectivaInit"] == null)
                    Session["AdminFasesContrato_FechaEfectivaInit"] = DateTime.Now;

                return Convert.ToDateTime(Session["AdminFasesContrato_FechaEfectivaInit"]);
            }
            set
            {
                Session["AdminFasesContrato_FechaEfectivaInit"] = value;
            }
        }

        public int NumeroFases
        {
            get
            {
                return txtNumeroFases.ValueInt;
            }
            set
            {
                txtNumeroFases.ValueInt = value;
            }
        }

        public bool TieneFase0
        {
            get
            {
                return Convert.ToBoolean(rblAplicado.SelectedValue);
            }
            set
            {
                rblAplicado.SelectedValue = value.ToString();
            }
        }

        public int MinDuracionFaseCero
        {
            get
            {
                if (Session["AdminFasesContrato_MinDuracionFaseCero"] == null)
                    Session["AdminFasesContrato_MinDuracionFaseCero"] = 1;

                return Convert.ToInt32(Session["AdminFasesContrato_MinDuracionFaseCero"]);
            }
            set
            {
                Session["AdminFasesContrato_MinDuracionFaseCero"] = value;
            }
        }

        public int MaxDuracionFaseCero
        {
            get
            {
                if (Session["AdminFasesContrato_MaxDuracionFaseCero"] == null)
                    Session["AdminFasesContrato_MaxDuracionFaseCero"] = 1;

                return Convert.ToInt32(Session["AdminFasesContrato_MaxDuracionFaseCero"]);
            }
            set
            {
                Session["AdminFasesContrato_MaxDuracionFaseCero"] = value;
            }
        }

        public int MinDuracionFaseExploratorio
        {
            get
            {
                if (Session["AdminFasesContrato_MinDuracionFaseExploratorio"] == null)
                    Session["AdminFasesContrato_MinDuracionFaseExploratorio"] = 1;

                return Convert.ToInt32(Session["AdminFasesContrato_MinDuracionFaseExploratorio"]);
            }
            set
            {
                Session["AdminFasesContrato_MinDuracionFaseExploratorio"] = value;
            }
        }

        public int MaxDuracionFaseExploratorio
        {
            get
            {
                if (Session["AdminFasesContrato_MaxDuracionFaseExploratorio"] == null)
                    Session["AdminFasesContrato_MaxDuracionFaseExploratorio"] = 1;

                return Convert.ToInt32(Session["AdminFasesContrato_MaxDuracionFaseExploratorio"]);
            }
            set
            {
                Session["AdminFasesContrato_MaxDuracionFaseExploratorio"] = value;
            }
        }

        public int MaxTotalDuracionFaseExploratorio
        {
            get
            {
                if (Session["AdminFasesContrato_MaxTotalDuracionFaseExploratorio"] == null)
                    Session["AdminFasesContrato_MaxTotalDuracionFaseExploratorio"] = 1;

                return Convert.ToInt32(Session["AdminFasesContrato_MaxTotalDuracionFaseExploratorio"]);
            }
            set
            {
                Session["AdminFasesContrato_MaxTotalDuracionFaseExploratorio"] = value;
            }
        }

        #endregion

        #endregion
    }
}