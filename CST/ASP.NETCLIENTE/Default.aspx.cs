using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Image = System.Web.UI.WebControls.Image;


namespace ASP.NETCLIENTE
{
    public partial class _Default : Page// ViewPage<ModulesPresenter, IModulesView>, IModulesView
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Modules/Contratos/Views/GeneralContractList.aspx?ModuleId=27");
        }

       

        //private long _lCurrentRecord = 0;
        //private const long LRecordsPerRow = 4;
        //protected void RptListadoItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType != ListItemType.Header & e.Item.ItemType != ListItemType.Footer)
        //    {
        //        for (var i = 0; i <= e.Item.Controls.Count - 1; i++)
        //        {

        //            var oModule = e.Item.DataItem as TBL_Maestra_Modulos;
        //            if(oModule == null) return;
        //            var img = e.Item.FindControl("imgModule") as Image;
        //            var hpModule = e.Item.FindControl("hpModule") as HyperLink;

        //            if (oModule.TBL_Maestra_Secciones.Any(x => x.TBL_Maestra_ModuleType.AutoActivar))
        //            {
        //                if (img != null && hpModule != null)
        //                {
        //                    img.ImageUrl = oModule.Imagen;
        //                    hpModule.NavigateUrl = string.Format("{0}?ModuleId={1}", oModule
        //                        .TBL_Maestra_Secciones.Select(x => x.TBL_Maestra_ModuleType.path)
        //                        .SingleOrDefault(),oModule.IdModulo);
        //                    hpModule.Text = oModule.NombreModulo;
        //                }
        //            }
        //            else
        //            {
                        
        //                if (img != null && hpModule != null)
        //                {
        //                    hpModule.Text = oModule.NombreModulo;
        //                    hpModule.Enabled = false;
        //                    hpModule.ForeColor = ColorTranslator.FromHtml("#808080");
        //                    img.ImageUrl = string.IsNullOrEmpty(oModule.ImageDisabled) ? 
        //                        "~/Resources/Images/NoDisponible.png" : 
        //                        oModule.ImageDisabled;
        //                }
        //            }
        //            var obLiteral = e.Item.Controls[i] as Literal;
        //            if (obLiteral == null) continue;
                   

        //            if (obLiteral.ID != "litRowStart") continue;
        //            _lCurrentRecord += 1;
        //            if ((_lCurrentRecord == 1))
        //            {
        //                obLiteral.Text = @"<tr>";
        //            }
        //            break;
        //        }

        //        for (var i = 0; i <= e.Item.Controls.Count - 1; i++)
        //        {
        //            var obLiteral = e.Item.Controls[i] as Literal;
        //            if (obLiteral != null)
        //            {
        //                if (obLiteral.ID == "litRowEnd")
        //                {
        //                    if (_lCurrentRecord % LRecordsPerRow == 0)
        //                    {
        //                        obLiteral.Text = @"</tr>";
        //                        _lCurrentRecord = 0;
        //                    }
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}

        //public void GetModulesList(List<TBL_Maestra_Modulos> items)
        //{
        //    rptListado.DataSource = items;
        //    rptListado.DataBind();
        //}
        //public void GetModulesList(List<TBL_Maestra_Modulos> items)
        //{
        //    var mainList = new HtmlGenericControl("ul");
        //    mainList.Attributes.Add("Id", "foo2");

        //    foreach (var ctr in items.Select(BuildListItemFromNode))
        //    {
        //        mainList.Controls.Add(ctr);
        //    }

        //    phLista.Controls.Add(mainList);
        //}


        //private static HtmlControl BuildListItemFromNode(TBL_Maestra_Modulos modulo )
        //{
        //    var listItem = new HtmlGenericControl("li");
        //    var hpl = new HyperLink { Text = modulo.NombreModulo };
        //    var img = new Image { ToolTip = modulo.NombreModulo };
        

        //    if (modulo.TBL_Maestra_Secciones.Any(x => x.TBL_Maestra_ModuleType.AutoActivar))
        //    {
        //        hpl.NavigateUrl = string.Format("{0}?ModuleId={1}", modulo
        //                        .TBL_Maestra_Secciones.Select(x => x.TBL_Maestra_ModuleType.path)
        //                        .SingleOrDefault(), modulo.IdModulo);
        //        img.ImageUrl = modulo.Imagen;
        //    }
        //    else
        //    {
        //        img.ImageUrl = modulo.ImageDisabled;
        //        hpl.Attributes.Add("href", "#");
        //    }
        //    hpl.Controls.Add(img);
        //    listItem.Controls.Add(hpl);

        //    return listItem;
        //}
       
    }
}
