//------------------------------------------------------------------------------
// <auto-generated>
//     Este codigo fue generado por el motor de generacion de codigo de propiedad de Walter molano.
//     El cambio  de algunas lineas de codigo podran causar comportamientos
//     inesperados de la aplicacion.  junio 18 de 2014.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.DocumentLibrary.Contracts;
using Applcations.MainModule.DocumentLibrary.IServices;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Domain.MainModule.DocumentLibrary.Spec;

namespace Applcations.MainModule.DocumentLibrary.Services
{
    public class SfTBL_ModuloDocumentosAnexos_CarpetasManagementServices : ISfTBL_ModuloDocumentosAnexos_CarpetasManagementServices
    {

         #region Fields
         readonly ITBL_ModuloDocumentosAnexos_CarpetasRepository _TBLModuloDocumentosAnexosCarpetasRepository;
         private readonly Infraestructure.Data.Core.ISqlHelper _sqlservice;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloDocumentosAnexos_CarpetasManagementServices(ITBL_ModuloDocumentosAnexos_CarpetasRepository TBLModuloDocumentosAnexosCarpetasRepository, Infraestructure.Data.Core.ISqlHelper sqlservice)
         {
            if (TBLModuloDocumentosAnexosCarpetasRepository == null)
                throw new ArgumentNullException("TBLModuloDocumentosAnexosCarpetasRepository");
            _TBLModuloDocumentosAnexosCarpetasRepository = TBLModuloDocumentosAnexosCarpetasRepository;
            _sqlservice = sqlservice;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloDocumentosAnexos_Carpetas NewEntity()
         {
            return new TBL_ModuloDocumentosAnexos_Carpetas();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloDocumentosAnexos_Carpetas entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloDocumentosAnexosCarpetasRepository.UnitOfWork;
            _TBLModuloDocumentosAnexosCarpetasRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloDocumentosAnexos_Carpetas entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TBLModuloDocumentosAnexosCarpetasRepository.UnitOfWork;
            _TBLModuloDocumentosAnexosCarpetasRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloDocumentosAnexos_Carpetas entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloDocumentosAnexosCarpetasRepository.UnitOfWork;

            _TBLModuloDocumentosAnexosCarpetasRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloDocumentosAnexos_Carpetas FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloDocumentosAnexos_Carpetas> specification = new DirectSpecification<TBL_ModuloDocumentosAnexos_Carpetas>(u => u.IdFolder == id);

            return _TBLModuloDocumentosAnexosCarpetasRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public TBL_ModuloDocumentosAnexos_Carpetas FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TBL_ModuloDocumentosAnexos_Carpetas> specification = new DirectSpecification<TBL_ModuloDocumentosAnexos_Carpetas>(u => u.Code == id);

            return _TBLModuloDocumentosAnexosCarpetasRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloDocumentosAnexos_Carpetas> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloDocumentosAnexos_Carpetas> specification = new DirectSpecification<TBL_ModuloDocumentosAnexos_Carpetas>(u => u.IsActive == isActive);
            return _TBLModuloDocumentosAnexosCarpetasRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloDocumentosAnexos_Carpetas> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloDocumentosAnexos_Carpetas> onlyEnabledSpec = new DirectSpecification<TBL_ModuloDocumentosAnexos_Carpetas>(u => u.IsActive);

            return _TBLModuloDocumentosAnexosCarpetasRepository.GetPagedElements(pageIndex, pageCount, u => u.CreatedOn, onlyEnabledSpec, true).ToList();
         }


         public List<TBL_ModuloDocumentosAnexos_Carpetas> GetFoldersByIdContrato(string IdContrato)
         {

             var onlyEnabledSpec = new CarpetasPorContratoCodeSpecifications(IdContrato);

             return _TBLModuloDocumentosAnexosCarpetasRepository.GetBySpec(onlyEnabledSpec).ToList();
         }

         public bool SaveFolder(string idParent, string idcategory, string nombre, string IdContrato, string idUser)
         {
             var txSettings = new TransactionOptions()
             {
                 Timeout = TransactionManager.DefaultTimeout,
                 IsolationLevel = System.Transactions.IsolationLevel.Serializable
             };

             using (var scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
             {
                 var unitOfWork = _TBLModuloDocumentosAnexosCarpetasRepository.UnitOfWork;

                 var oFolder = NewEntity();
                 oFolder.CreatedBy = idUser;
                 oFolder.CreatedOn = DateTime.Now;
                 oFolder.IdContrato = Convert.ToInt32(IdContrato);
                 if (!string.IsNullOrEmpty(idParent))
                     oFolder.IdParent = Convert.ToInt32(idParent);
                 oFolder.IsActive = true;
                 oFolder.Nombre = nombre;
                 oFolder.ModifiedBy = idUser;
                 oFolder.ModifiedOn = DateTime.Now;

                 _TBLModuloDocumentosAnexosCarpetasRepository.Add(oFolder);

                 unitOfWork.Commit();

                 scope.Complete();

                 return true;
             }


         }


         public bool DeleteFolderAndFiles(int idFolder)
         {
             _sqlservice.ExecuteNonquery("EliminarCarpeta", CommandType.StoredProcedure, new SqlParameter("@IdFolder", idFolder));
             return true;
         }

         #endregion

         #region IDisposable Members

        /// <summary>
        /// Release all resources
        /// </summary>
        public void Dispose()
        {
            //release used unit of work
            //if you have many repositories but  lifetime is per resolve only need
            //dispose one

            if (_TBLModuloDocumentosAnexosCarpetasRepository != null)
            {
                _TBLModuloDocumentosAnexosCarpetasRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    