using System;
using Application.Core;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;

namespace Presenters.Admin.Presenters
{
    public class EditModulePresenter : Presenter<IEditModulesView>
    {

        private readonly ISfTBL_Admin_TypeByModulesManagementServices _seccionesServices;
        private readonly ISfTBL_Admin_ModulosManagementServices _modulesServices;
        private readonly ISfTBL_Admin_ModuleTypeManagementServices _componentesServices;

        public EditModulePresenter(
            ISfTBL_Admin_TypeByModulesManagementServices seccionesServices, 
            ISfTBL_Admin_ModulosManagementServices modulesServices,
            ISfTBL_Admin_ModuleTypeManagementServices componentesServices)
        {
            _seccionesServices = seccionesServices;
            _componentesServices = componentesServices;
            _modulesServices = modulesServices;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.SelectedModuleEvent += ViewSelectedModuleEvent;
            View.EditSectionEvent += ViewEditSectionEvent;
            View.SaveEvent += ViewSaveEvent;
            View.DeleteEvent += ViewDeleteEvent;
            View.ModulesEvent += ViewModulesEvent;
            View.ComponentsEvent += ViewComponentsEvent;
        }

        
        

        #region Eventos

        void ViewComponentsEvent(object sender, EventArgs e)
        {
            ListadComponentes();
        }

        void ViewModulesEvent(object sender, EventArgs e)
        {
            ListadoModulosBySecciones();
        }

        void ViewDeleteEvent(object sender, EventArgs e)
        {
            Delete();
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(View.IdSeccion))
               Save();
           else
                Update(Convert.ToInt32(View.IdSeccion));
        }

        void ViewEditSectionEvent(object sender, EventArgs e)
        {
           if(sender == null)return;
            SelectedSectionById(string.IsNullOrEmpty(sender.ToString()) ?-1: Convert.ToInt32(sender));
        }

        void ViewSelectedModuleEvent(object sender, EventArgs e)
        {
            if(sender==null)return;
            SelectedSectionBymodule(Convert.ToInt32(sender));
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if(View.IsPostBack)return;
            ListadoModulos();
        }


        #endregion

        #region Metodos

        private void ListadComponentes()
        {
            try
            {
                var list = _componentesServices.FindBySpec(true);
                View.ListadoComponentesSeccion(list);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        private  void ListadoModulos()
        {
            try
            {
                var list = _modulesServices.FindBySpec(true);
                View.ListadoModulos(list);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        private void ListadoModulosBySecciones()
        {
            try
            {
                var list = _modulesServices.FindBySpec(true);
                View.ListadoModulosSeccion(list);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        private void SelectedSectionBymodule(int idModule)
        {
            try
            {
                var list = _seccionesServices.ListadoSeccionesPormodulo(idModule);
                View.ListadoSecciones(list);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        private void SelectedSectionById(int idSeccion)
        {
            try
            {
                var list = _seccionesServices.SeccionporId(idSeccion);
                View.CargarSeccionById(list);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        private void Save()
        {
            try
            {
                var oSection = _seccionesServices.NewEntity();
                oSection.CreateBy = View.UserSession.IdUser.ToString();
                oSection.CreateOn = DateTime.Now;
                oSection.IdModuleType = View.IdComponente;
                oSection.IdModulo = View.IdModulo;
                oSection.ModifiedBy = View.UserSession.IdUser.ToString();
                oSection.ModifiedOn = DateTime.Now;
                oSection.MostrarTitulo = View.MostrarTitulo;
                oSection.Placeholder = View.PlaceHolder;
                oSection.position = View.Position;
                oSection.Titulo = View.Titulo;
                oSection.IsActive = View.Activar;
                _seccionesServices.Add(oSection);

                if (oSection.IdModulo.HasValue)
                UpdatePathModule(oSection.IdModulo.GetValueOrDefault());
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }
        }

        private void Update(int idSeccion)
        {
            try
            {
                var oSection = _seccionesServices.FindById(idSeccion);
                if(oSection == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Sección"), TypeError.Error));
                    return;
                }

                oSection.IdModuleType = View.IdComponente;
                oSection.IdModulo = View.IdModulo;
                oSection.IsActive = true;
                oSection.ModifiedBy = View.UserSession.IdUser.ToString();
                oSection.ModifiedOn = DateTime.Now;
                oSection.MostrarTitulo = View.MostrarTitulo;
                oSection.Placeholder = View.PlaceHolder;
                oSection.position = View.Position;
                oSection.Titulo = View.Titulo;
                oSection.IsActive = View.Activar;
                _seccionesServices.Modify(oSection);

                if (oSection.IdModulo.HasValue)
                UpdatePathModule(oSection.IdModulo.GetValueOrDefault());

                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.EditError), TypeError.Error));
            }
        }

        private void Delete()
        {
            try
            {
                if(string.IsNullOrEmpty(View.IdSeccion))return;

                var oSection = _seccionesServices.FindById( Convert.ToInt32( View.IdSeccion) );
                if (oSection == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Sección"), TypeError.Error));
                    return;
                }
                
                _seccionesServices.Remove(oSection);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.DeleteError), TypeError.Error));
            }
        }

        private void UpdatePathModule(int idModule)
        {
            try
            {
                var oModule = _modulesServices.FindById(idModule);
                if(oModule != null)
                {
                    oModule.PathFormPreView = View.PathPreView;
                    _modulesServices.Modify(oModule);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        #endregion
    }
}