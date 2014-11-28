using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Services.InstallerDb;
using Presenters.Admin.IViews;
using Presenters.Admin.Presenters;
using ServerControls;

namespace Modules.Admin.Catalogos
{
    public partial class FrmAdminModules : ViewPage<ModulesPresenter, IModulesView>, IModulesView
    {
        public event EventHandler FilterEvent;
        public event EventHandler UpdateEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Administración de Modulos de la Aplicación.");
        }

        protected void PgrListadoPageChanged(object sender, PageChanged e)
        {
            if (FilterEvent != null)
                FilterEvent(e.CurrentPage, EventArgs.Empty);
        }

        protected void ChkBoxActivationCheckedChanged(object sender, EventArgs e)
        {
            TBL_Admin_ModuleType mt = null;
            try
            {
                var box = (CheckBox)sender;
                if (box.InputAttributes["moduleTypeId"] != null)
                {
                    mt = Presenter.GetById(int.Parse(box.InputAttributes["moduleTypeId"]));
                    if(mt == null)return;
                    if (box.Checked)
                    {
                        mt.AutoActivar = true;
                        if (UpdateEvent != null)
                            UpdateEvent(mt, EventArgs.Empty);
                       //Activamos en modulo dentro del container
                        LoaderModule.ActivateModule(mt);
                    }
                    else
                    {
                        //Inactivamos el modulo
                        mt.AutoActivar = false;
                        if (UpdateEvent != null)
                            UpdateEvent(mt, EventArgs.Empty);
                    }
                }

                if (FilterEvent != null)
                    FilterEvent(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                if (mt != null) ShowError("Loading failed for " + mt.Nombre + ".<br/>" + ex.Message);
                else ShowError("Loading failed for module.<br/>" + ex.Message);
            }
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public void GetModules(List<TBL_Admin_ModuleType> modules)
        {

            var moduleRootDir = HttpContext.Current.Server.MapPath("~/Pages/Modules");
            var moduleDirectories = new DirectoryInfo(moduleRootDir).GetDirectories();

            foreach (var di in moduleDirectories)
            {
                var shouldAdd = (di.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden;
                if (modules.Any(moduleType => moduleType.Nombre == di.Name))
                {
                    shouldAdd = false;
                }

                if (!shouldAdd) continue;
                var newModuleType = new TBL_Admin_ModuleType {Nombre = di.Name};
                modules.Add(newModuleType);
            }

            rptModules.DataSource = modules;
            rptModules.DataBind();
        }

        public int PageZise
        {
            get { return pgrListado.PageSize; }
        }

        public int TotalRegistrosPaginador
        {
            set { pgrListado.RowCount = value; }
        }


        protected void RptModulesItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var moduleType = e.Item.DataItem as TBL_Admin_ModuleType;
                if (moduleType == null) return;

                var physicalModuleInstallDirectory =
                    Path.Combine(Server.MapPath("~/Pages/Modules/" + moduleType.Nombre), "Install");
                Assembly moduleAssembly = null;
                if (moduleType.NombreEnsamblado != null)
                {
                    moduleAssembly = Assembly.Load(moduleType.NombreEnsamblado);
                }
                var dbInstaller = new DatabaseInstaller(physicalModuleInstallDirectory, moduleAssembly);
                var canInstall = dbInstaller.CanInstall;
                var canUpgrade = dbInstaller.CanUpgrade;
                var canUninstall = dbInstaller.CanUninstall;

                var lbtInstall = e.Item.FindControl("lbtInstall") as LinkButton;
                if (lbtInstall != null)
                {
                    lbtInstall.Visible = canInstall;
                    lbtInstall.Attributes.Add("onclick", "return confirm('Install this module?')");
                }

                var lbtUpgrade = e.Item.FindControl("lbtUpgrade") as LinkButton;
                if (lbtUpgrade != null)
                {
                    lbtUpgrade.Visible = canUpgrade;
                    lbtUpgrade.Attributes.Add("onclick", "return confirm('Upgrade this module?')");
                }

                var lbtUninstall = e.Item.FindControl("lbtUninstall") as LinkButton;
                if (lbtUninstall != null)
                {
                    lbtUninstall.Visible = canUninstall;
                    lbtUninstall.Attributes.Add("onclick", "return confirm('Uninstall this module?')");
                }

                var chkBox = e.Item.FindControl("chkBoxActivation") as CheckBox;
                if (chkBox != null)
                {
                    if (canInstall)
                    {
                        chkBox.Enabled = false;
                        chkBox.Checked = moduleType.AutoActivar;
                    }
                    else
                    {
                        chkBox.Enabled = true;
                        chkBox.Checked = moduleType.AutoActivar;
                        if (moduleType.Nombre != null)
                            chkBox.InputAttributes.Add("moduleTypeId", moduleType.IdModuleType.ToString());
                    }
                }

                var litActivationStatus = e.Item.FindControl("litActivationStatus") as Literal;
                if (LoaderModule.IsModuleActive(moduleType))
                {
                    if (litActivationStatus != null)
                        litActivationStatus.Text = @"<span style=""color:green;"">Active</span>";
                }
                else
                {
                    if (litActivationStatus != null)
                        litActivationStatus.Text = @"<span style=""color:red;"">Not Active</span>";
                }


                var litStatus = e.Item.FindControl("litStatus") as Literal;
                if (dbInstaller.CurrentVersionInDatabase != null)
                {
                    if (litStatus != null)
                    {
                        litStatus.Text = String.Format("Installed ({0}.{1}.{2})"
                                                       , dbInstaller.CurrentVersionInDatabase.Major
                                                       , dbInstaller.CurrentVersionInDatabase.Minor
                                                       , dbInstaller.CurrentVersionInDatabase.Build);
                        if (dbInstaller.CanUpgrade)
                        {
                            litStatus.Text += @" (upgrade available) ";
                        }
                    }
                }
                else
                {
                    if (litStatus != null) litStatus.Text = @"Uninstalled";
                }

            }
        }

        protected void RptModulesItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var commandArguments = e.CommandArgument.ToString().Split(':');
            var moduleName = commandArguments[0];
            var assemblyName = commandArguments[1];
            Assembly assembly = null;
            if (assemblyName.Length > 0)
            {
                assembly = Assembly.Load(assemblyName);
            }

            var moduleInstallDirectory = Path.Combine(Server.MapPath("~/Pages/Modules/" + moduleName), "Install");
            var dbInstaller = new DatabaseInstaller(moduleInstallDirectory, assembly);

            try
            {
                switch (e.CommandName.ToLower())
                {
                    case "install":
                        dbInstaller.Install();
                        break;
                    case "upgrade":
                        dbInstaller.Upgrade();
                        break;
                    case "uninstall":
                        dbInstaller.Uninstall();
                        break;
                }

                if (FilterEvent != null)
                    FilterEvent(null, EventArgs.Empty);

                
                ShowMessageOk(e.CommandName + ": operation succeeded for " + moduleName + ".");
            }
            catch (Exception ex)
            {
                ShowError(e.CommandName + ": operation failed for " + moduleName + ".<br/>" + ex.Message);
            }
        }
       



    }
}