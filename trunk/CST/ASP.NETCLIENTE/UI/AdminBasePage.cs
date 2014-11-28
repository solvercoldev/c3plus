namespace ASP.NETCLIENTE.UI
{
    public class AdminBasePage : GenericBasePage
    {
        protected override void OnInit(System.EventArgs e)
        {
            TemplateDir = "~/Template/WPC/";
            TemplateFilename = "AdminTemplate.ascx";
            Css = "~/Template/WPC/css/style.css";
            base.OnInit(e);
        }
    }
}