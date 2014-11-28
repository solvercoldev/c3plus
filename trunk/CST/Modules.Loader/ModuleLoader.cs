using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Application.Core;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting;
using Infrastructure.CrossCutting.IoC;
using Infrastructure.CrossCutting.Logging;
using Infrastructure.CrossCutting.NetFramework.Util;

namespace Modules.Loader
{
    public class ModuleLoader
    {
        private readonly object LockObject = "";
        private readonly ISfTBL_Admin_ModuleTypeManagementServices _iSfModuleTypeManagementServices;
        private readonly ITraceManager _traceManager;
        public ModuleLoader(ISfTBL_Admin_ModuleTypeManagementServices iSfModuleTypeManagementServices, ITraceManager traceManager)
        {
            _iSfModuleTypeManagementServices = iSfModuleTypeManagementServices;
            _traceManager = traceManager;
        }

        /// <summary>
        /// Activate all modules that have the AutoActivate property set to true
        /// </summary>
        public void RegisterActivatedModules()
        {

            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application["IsModuleLoading"] = true;
            HttpContext.Current.Application.UnLock();

            IEnumerable<TBL_Admin_ModuleType> moduleTypes = _iSfModuleTypeManagementServices.FindAllModuleType(true);
            foreach (var mt in moduleTypes.Where(mt => mt.AutoActivar))
            {
                ActivateModule(mt);
            }

            // Let the application know that the modules are loaded.
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application["ModulesLoaded"] = true;
            HttpContext.Current.Application["IsModuleLoading"] = false;
            HttpContext.Current.Application.UnLock();

        }

        public void ActivateModule(TBL_Admin_ModuleType moduleType)
        {

            // System.Threading.Monitor.Enter(LockObject);

            var assemblyQualifiedName = moduleType.NombreClase + ", " + moduleType.NombreEnsamblado;

            // First, try to get the CLR module type
            var moduleTypeType = Type.GetType(assemblyQualifiedName);

            if (moduleTypeType == null) return;

            try
            {
                // double check, if we should continue
                if (IoC.HasComponent(moduleTypeType))
                {
                    // Module is already registered
                    _traceManager.LogInfo(string.Format("El modulo {0} ya esta registrado en el contenedor.", moduleTypeType), LogType.Notify);
                    return;
                }

                //Registramos los repositorios asociados a los modulos.
                foreach (var repoService in moduleType.TBL_Admin_ModuleRepository)
                {
                    var serviceType = Type.GetType(repoService.RepositoryType);
                    var classType = Type.GetType(repoService.classtype);
                    if(serviceType ==  null || classType==null)continue;
                    _traceManager.LogInfo(string.Format("Loading module repository {0}, {1}.", repoService.Repositorykey, repoService.classtype), LogType.Notify);
                    IoC.RegisterType(serviceType, classType);
                }

                // First, register optional module services that the module might depend on.
                foreach (var moduleService in moduleType.TBL_Admin_ModuleService)
                {
                    var serviceType = Type.GetType(moduleService.servicetype);
                    var classType = Type.GetType(moduleService.classtype);
                    if (serviceType == null || classType == null) continue;
                    _traceManager.LogInfo(string.Format("Loading module service {0}, {1}.", moduleService.servicekey, moduleService.classtype), LogType.Notify);
                    IoC.RegisterType(serviceType, classType);
                    
                }

                //Register the module
                //var moduleTypeKey = "module." + moduleTypeType.FullName;
                IoC.RegisterType(moduleTypeType); // no lifestyle because ModuleBase has the Transient attribute.
                _traceManager.LogInfo(string.Format("Adding module assembly {0} to the Container.", moduleTypeType.Assembly), LogType.Notify);
                //Configure NHibernate mappings and make sure we haven't already added this assembly to the NHibernate config
                if ((HttpContext.Current.Application[moduleType.NombreEnsamblado]) == null)
                {
                    //set application variable to remember the configurated assemblies
                    HttpContext.Current.Application.Lock();
                    HttpContext.Current.Application[moduleType.NombreEnsamblado] = moduleType.NombreEnsamblado;
                    HttpContext.Current.Application.UnLock();
                }

            }
            catch (Exception ex)
            {
                _traceManager.LogInfo(string.Format("Error al cargar el ensamblado al IOC. Error Tecnico : {0}", ex.Message), LogType.Notify);
            }

        }//end method

        /// <summary>
        /// Get the module instance that is associated with the given section.
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public ModuleBase GetModuleFromSection(TBL_Admin_TypeByModules section)
        {


            var module = GetModuleFromType(section.TBL_Admin_ModuleType);
            if (module != null)
            {
                module.Seccion = section;
                if (HttpContext.Current != null)
                {
                    module.SectionUrl = UrlHelper.GetUrlFromSection(section.OID.ToString());
                }
              //  module.ReadSectionSettings();
            }
            return module;
        }


        /// <summary>
        /// Get the module instance by its type
        /// </summary>
        /// <param name="moduleType"></param>
        public ModuleBase GetModuleFromType(TBL_Admin_ModuleType moduleType)
        {
            if (moduleType != null)
            {
                var st = Type.GetType(moduleType.NombreClase + "," + moduleType.NombreEnsamblado);
                if (IoC.HasComponent(st))
                {
                    return (ModuleBase)IoC.Resolve(st);
                }
            }
            return null;
        }

        /// <summary>
        /// Get the module instance by its type
        /// </summary>
        /// <param name="serviceType"></param>
        private static ModuleBase GetModuleFromType(string serviceType)
        {
            //string modulekey = string.Concat("module.", moduleType.ClassName);
            var st = Type.GetType(serviceType);

            if (IoC.HasComponent(st))
            {
                return (ModuleBase)IoC.Resolve(st);
            }
            return null;
        }


        public bool IsModuleActive(TBL_Admin_ModuleType moduleType)
        {
            var assemblyQualifiedName = moduleType.NombreClase + ", " + moduleType.NombreEnsamblado;
            var moduleTypeType = Type.GetType(assemblyQualifiedName);
            if (moduleTypeType == null) return false;
            return IoC.HasComponent(moduleTypeType);
        }

        //public Type LoadPageFromAssemby()
        //{
        //    var ensamble = Assembly.Load("Modules.Pedidos");
        //    var t = ensamble.GetType("Modules.Pedidos.Catalogos.FrmEditarPedido");
        //    return t;
        //}

    }
}