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
    public partial class FrmNewCompromisoContrato : ViewPage<NewCompromisoContratoPresenter, INewCompromisoContratoView>, INewCompromisoContratoView
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
            ImprimirTituloVentana(string.Format("Nuevo Compromiso - "));
            ImprimirAuxTituloVentana("Registro");
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        #endregion

        #region TreeView

        protected void ManualesSelect_Change(object sender, EventArgs e)
        {
            var manual = ManualesANH.Where(x => x.IdManualAnh == tvManualANH.SelectedValue).FirstOrDefault();

            ManualId = manual.IdManualAnh;
            ManualNoProducto = string.Format("{0}", manual.NumeroProducto);
            ManualProducto = string.Format("{0}", manual.Producto);
            ManualContenido = string.Format("{0}", manual.Contenido);
            ManualFormato = string.Format("{0}", manual.Formato);
            ManualMedio = string.Format("{0}", manual.Medio);
            ManualEntrega = string.Format("{0}", manual.Entrega);

            tvManualANH.SelectedNode.Selected = false;

            ShowAdminEntregable(true);
        }

        #endregion

        #region Buttons

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            GoToContratoView();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            var messages = new List<string>();

            if (string.IsNullOrEmpty(Nombre))
                messages.Add(string.Format("Es necesario ingresar un nombre para el compromiso."));

            if (string.IsNullOrEmpty(Descripcion))
                messages.Add(string.Format("Es necesario ingresar una descripción para el compromiso."));

            switch (TipoAsociacion)
            {
                case "Entregable":
                    if(!SelectedManualesANH.Any())
                        messages.Add(string.Format("Es necesario ingresar por lo menos un entregable ANH asociado al pago obligación."));
                    break;
                case "Pago":
                    if (ValorPago <= 0)
                        messages.Add(string.Format("El valor del pago debe ser mayo a 0."));
                    break;
            }

            if (messages.Any())
            {
                AddErrorMessages(messages);
                return;
            }

            Presenter.SaveCompromiso();
        }

        protected void BtnAddEntregable_Click(object sender, EventArgs e)
        {
            InitInfoManual();
            ShowAdminEntregable(true);
        }

        protected void BtnRemoveEntregable_Click(object sender, EventArgs e)
        {
            if (lstEntregablesANH.SelectedItem != null)
            {
                var idEntregable = lstEntregablesANH.SelectedValue;

                var item = SelectedManualesANH.Where(x => x.Id == idEntregable).FirstOrDefault();

                SelectedManualesANH.Remove(item);

                LoadSelectedManuales();
            }
        }


        protected void BtnSaveEntregable_Click(object sender, EventArgs e)
        {
            PopulateSelectedManuales();
            LoadSelectedManuales();
        }

        #endregion

        #region DropDownList

        protected void TxtDiasAlarma_TextChanged(object sender, EventArgs e)
        {
            CheckAlarmaText();
        }

        void CheckAlarmaText()
        {
            lblFechaAlarma.Text = string.Format("{0:dd/MM/yyyy}", FechaCumplimiento.AddDays(DiasAlarma * (-1)));
        }

        #endregion

        #region DropDownList

        protected void DdlFase_IndexChanged(object sender, EventArgs e)
        {
            SetInfoFase();
        }

        void SetInfoFase()
        {
            var fase = FasesContrato.Where(x => x.IdFase == IdFase).First();

            lblPeriodoFase.Text = string.Format("{0:dd/MM/yyyy} - {1:dd/MM/yyyy}", fase.FechaInicio, fase.FechaFinalizacion);
            cexTxtfechaCompromiso.StartDate = fase.FechaInicio;
            cexTxtfechaCompromiso.EndDate = fase.FechaFinalizacion;
            FechaCumplimiento = fase.FechaInicio;

            CheckAlarmaText();
        }

        #endregion

        #region RadioButton

        protected void RblAsociadoA_IndexChanged(object sender, EventArgs e)
        {
            switch (AsociadoA)
            {
                case "Bloque":
                    Presenter.LoadBloque();
                    break;
                case "Campo":
                    Presenter.LoadCampos();
                    break;
                case "Pozo":
                    Presenter.LoadPozos();
                    break;
            }
        }

        protected void RblAsociarCompromiso_IndexChanged(object sender, EventArgs e)
        {
            switch (TipoAsociacion)
            {
                case "No":
                    tblEntregablesANH.Visible = false;
                    tblPagosObligaciones.Visible = false;
                    break;
                case "Entregable":
                    tblEntregablesANH.Visible = true;
                    tblPagosObligaciones.Visible = false;
                    break;
                case "Pago":
                    ValorPago = 0;
                    ValorCoberturaPago = 0;
                    NumeroDocumentoPago = string.Empty;
                    ddlTipoPago.SelectedIndex = 0;
                    ddlEntidad.SelectedIndex = 0;
                    ddlMoneda.SelectedIndex = 0;
                    ddlMonedaCobertura.SelectedIndex = 0;
                    tblEntregablesANH.Visible = false;
                    tblPagosObligaciones.Visible = true;
                    break;
            }
        }

        #endregion

        #endregion

        #region Methods

        void InitInfoManual()
        {
            ManualId = string.Empty;
            ManualNoProducto = string.Empty;
            ManualProducto = string.Empty;
            ManualContenido = string.Empty;
            ManualFormato = string.Empty;
            ManualMedio = string.Empty;
            ManualEntrega = string.Empty;
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

        public void ShowAdminEntregable(bool visible)
        {
            if (visible)
                mpeAdminEntregableANH.Show();
            else
                mpeAdminEntregableANH.Hide();
        }

        public void LoadFases(List<Fases> items)
        {
            ddlFase.DataSource = items;
            ddlFase.DataTextField = "Nombre";
            ddlFase.DataValueField = "IdFase";
            ddlFase.DataBind();

            FasesContrato = items;

            SetInfoFase();
        }

        public void LoadResponsables(List<TBL_Admin_Usuarios> items)
        {
            ddlResponsable.DataSource = items;
            ddlResponsable.DataTextField = "Nombres";
            ddlResponsable.DataValueField = "IdUser";
            ddlResponsable.DataBind();
        }

        public void LoadTipoCompromiso(List<DTO_ValueKey> items)
        {
            ddlTipoCompromiso.DataSource = items;
            ddlTipoCompromiso.DataTextField = "Value";
            ddlTipoCompromiso.DataValueField = "Id";
            ddlTipoCompromiso.DataBind();
        }

        public void LoadImportancia(List<DTO_ValueKey> items)
        {
            ddlImportancia.DataSource = items;
            ddlImportancia.DataTextField = "Value";
            ddlImportancia.DataValueField = "Id";
            ddlImportancia.DataBind();
        }

        public void LoadBCP(List<DTO_ValueKey> items)
        {
            ddlBCP.DataSource = items;
            ddlBCP.DataTextField = "Value";
            ddlBCP.DataValueField = "Id";
            ddlBCP.DataBind();
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

        public void LoadManuales(List<ManualAnh> items)
        {
            if (items.Any())
            {
                var parents = items.Where(x => x.IdManualAnhPadre == "0").Distinct().OrderBy(x => x.IdManualAnh).ToList();

                foreach (var p in parents)
                {
                    TreeNode parentNode = new TreeNode(string.Format("{0} - {1}",p.IdManualAnh, p.Producto), p.IdManualAnh);
                    tvManualANH.Nodes.Add(parentNode);
                    tvManualANH.CollapseAll();

                    //parentNode.SelectAction = TreeNodeSelectAction.None;

                    AddTreeNode(parentNode, items);
                }

                ManualesANH = items;
            }
            
        }

        void LoadSelectedManuales()
        {
            lstEntregablesANH.DataSource = SelectedManualesANH;
            lstEntregablesANH.DataTextField = "Value";
            lstEntregablesANH.DataValueField = "Id";
            lstEntregablesANH.DataBind();
        }

        void AddTreeNode(TreeNode parent, List<ManualAnh> items)
        {
            var childs = items.Where(x => x.IdManualAnhPadre == parent.Value).Distinct().OrderBy(x => x.IdManualAnh).ToList();

            foreach (var c in childs)
            {
                TreeNode childNode = new TreeNode(string.Format("{0} - {1}", c.IdManualAnh, c.Producto), c.IdManualAnh);
                parent.ChildNodes.Add(childNode);
                childNode.CollapseAll();

                //childNode.SelectAction = TreeNodeSelectAction.None;

                AddTreeNode(childNode, items);
            }
        }

        public void PopulateSelectedManuales()
        {
            SelectedManualesANH = new List<DTO_ValueKey>();

            foreach (TreeNode tn in tvManualANH.Nodes)
            {
                if (tn.Checked)
                    SelectedManualesANH.Add(new DTO_ValueKey() { Id = tn.Value, Value = tn.Text });
                
                SetSelecctedChild(tn);
            }
        }

        void SetSelecctedChild(TreeNode parent)
        {
            foreach (TreeNode cn in parent.ChildNodes)
            {
                if (cn.Checked)
                    SelectedManualesANH.Add(new DTO_ValueKey() { Id = cn.Value, Value = cn.Text });

                SetSelecctedChild(cn);
            }
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
            }            
        }

        public void GoToCompromisoView(long idCompromiso)
        {
            Response.Redirect(string.Format("FrmAdminCompromisoContrato.aspx?ModuleId={0}&IdContrato={1}&IdCompromiso={2}&from={2}", ModuleId, IdContrato, idCompromiso, FromPage));
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

        public string TipoCompromiso
        {
            get
            {
                return ddlTipoCompromiso.SelectedValue;
            }
            set
            {
                ddlTipoCompromiso.SelectedValue = value;
            }
        }

        public string Nombre
        {
            get
            {
                return txtNombre.Text;
            }
            set
            {
                txtNombre.Text = value;
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
                return Convert.ToDateTime(txtFechaCompromiso.Text);
            }
            set
            {
                txtFechaCompromiso.Text = value.ToString("dd/MM/yyyy");
            }
        }

        public string Importancia
        {
            get
            {
                return ddlImportancia.SelectedValue;
            }
            set
            {
                ddlImportancia.SelectedValue = value;
            }
        }

        public string AsociadoA
        {
            get
            {
                return rblAsociadoA.SelectedValue;
            }
            set
            {
                rblAsociadoA.SelectedValue = value;
            }
        }

        public string BCP
        {
            get
            {
                return ddlBCP.SelectedValue;
            }
            set
            {
                ddlBCP.SelectedValue = value;
            }
        }

        public string Responsable
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

        public string TipoAsociacion
        {
            get
            {
                return rblAsociarCompromiso.SelectedValue;
            }
            set
            {
                rblAsociarCompromiso.SelectedValue = value;
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

        public string NumeroDocumentoPago
        {
            get
            {
                return txtPagoNumeroDocumento.Text;
            }
            set
            {
                txtPagoNumeroDocumento.Text = value;
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

        public List<DTO_ValueKey> SelectedManualesANH
        {
            get
            {
                if(Session["NewCompromisoContrato_SelectedManualesANH"] == null)
                    Session["NewCompromisoContrato_SelectedManualesANH"] = new List<DTO_ValueKey>();

                return Session["NewCompromisoContrato_SelectedManualesANH"] as List<DTO_ValueKey>;
            }
            set
            {
                Session["NewCompromisoContrato_SelectedManualesANH"] = value;
            }
        }

        public List<Fases> FasesContrato
        {
            get
            {
                if (Session["NewCompromisoContrato_FasesContrato"] == null)
                    Session["NewCompromisoContrato_FasesContrato"] = new List<Fases>();

                return Session["NewCompromisoContrato_FasesContrato"] as List<Fases>;
            }
            set
            {
                Session["NewCompromisoContrato_FasesContrato"] = value;
            }
        }

        public List<ManualAnh> ManualesANH
        {
            get
            {
                if (Session["NewCompromisoContrato_ManualesANH"] == null)
                    Session["NewCompromisoContrato_ManualesANH"] = new List<ManualAnh>();

                return Session["NewCompromisoContrato_ManualesANH"] as List<ManualAnh>;
            }
            set
            {
                Session["NewCompromisoContrato_ManualesANH"] = value;
            }
        }

        public string ManualId
        {
            get { return lblManualId.Text; }
            set { lblManualId.Text = value; }
        }

        public string ManualNoProducto
        {
            get { return lblManualNoProducto.Text; }
            set { lblManualNoProducto.Text = value; }
        }

        public string ManualProducto
        {
            get { return lblManualProducto.Text; }
            set { lblManualProducto.Text = value; }
        }

        public string ManualContenido
        {
            get { return txtManualContenido.Text; }
            set { txtManualContenido.Text = value; }
        }

        public string ManualFormato
        {
            get { return lblManualFormato.Text; }
            set { lblManualFormato.Text = value; }
        }

        public string ManualMedio
        {
            get { return lblManualMedio.Text; }
            set { lblManualMedio.Text = value; }
        }

        public string ManualEntrega
        {
            get { return lblManualEntrega.Text; }
            set { lblManualEntrega.Text = value; }
        }

        #endregion

        #endregion            
    }
}