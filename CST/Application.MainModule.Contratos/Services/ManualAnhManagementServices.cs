using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfManualAnhManagementServices : ISfManualAnhManagementServices
    {

         #region Fields
         readonly IManualAnhRepository _ManualAnhRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfManualAnhManagementServices( IManualAnhRepository ManualAnhRepository)
         {
            if (ManualAnhRepository == null)
                throw new ArgumentNullException("ManualAnhRepository");
            _ManualAnhRepository = ManualAnhRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public ManualAnh NewEntity()
         {
            return new ManualAnh();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(ManualAnh entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _ManualAnhRepository.UnitOfWork;
            _ManualAnhRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(ManualAnh entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _ManualAnhRepository.UnitOfWork;
            _ManualAnhRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(ManualAnh entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _ManualAnhRepository.UnitOfWork;

            _ManualAnhRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public ManualAnh FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<ManualAnh> specification = new DirectSpecification<ManualAnh>(u => u.IdManualAnh == id.ToString());

            return _ManualAnhRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public ManualAnh FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<ManualAnh> specification = new DirectSpecification<ManualAnh>(u => u.Code == id);

            return _ManualAnhRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<ManualAnh> FindBySpec(bool isActive)
         {
             Specification<ManualAnh> specification = new DirectSpecification<ManualAnh>(u => u.IdManualAnh != null);
            return _ManualAnhRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<ManualAnh> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<ManualAnh> onlyEnabledSpec = new DirectSpecification<ManualAnh>(u => u.IdManualAnh != null);

            return _ManualAnhRepository.GetPagedElements(pageIndex, pageCount, u => u.Producto, onlyEnabledSpec, true).ToList();
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

            if (_ManualAnhRepository != null)
            {
                _ManualAnhRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    