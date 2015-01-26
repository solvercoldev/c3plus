using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Application.Core;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Contratos.IViews;
using Presenters.Contratos.Presenters;

namespace Modules.Contratos.Catalogs
{
    public partial class FrmManualesANH : ViewPage<ManualesANHPresenter, IManualesANHView>, IManualesANHView
    {
        #region Members
       
        #endregion

        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana(string.Format("Manuales ANH"));
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
        }

        #endregion

        #region Buttons
      

        #endregion

        #region DropDownList      

        #endregion

        #region DropDownList      

        #endregion

        #region RadioButton

        #endregion

        #endregion

        #region Methods

        #endregion

        #region View Members

        #region Methods

        public void LoadManuales(List<ManualAnh> items)
        {
            if (items.Any())
            {
                var parents = items.Where(x => x.IdManualAnhPadre == "0").Distinct().OrderBy(x => x.IdManualAnh).ToList();

                foreach (var p in parents)
                {
                    TreeNode parentNode = new TreeNode(string.Format("{0} - {1}", p.IdManualAnh, p.Producto), p.IdManualAnh);
                    tvManualANH.Nodes.Add(parentNode);
                    tvManualANH.CollapseAll();

                    //parentNode.SelectAction = TreeNodeSelectAction.None;

                    AddTreeNode(parentNode, items);
                }

                ManualesANH = items;
            }

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

        public List<ManualAnh> ManualesANH
        {
            get
            {
                if (Session["AdminManualANH_ManualesANH"] == null)
                    Session["AdminManualANH_ManualesANH"] = new List<ManualAnh>();

                return Session["AdminManualANH_ManualesANH"] as List<ManualAnh>;
            }
            set
            {
                Session["AdminManualANH_ManualesANH"] = value;
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