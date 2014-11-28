using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfEstadosAccionManagementServices : ISfEstadosAccionManagementServices
    {

         #region Fields
         readonly IEstadosAccionRepository _EstadosAccionRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfEstadosAccionManagementServices( IEstadosAccionRepository EstadosAccionRepository)
         {
            if (EstadosAccionRepository == null)
                throw new ArgumentNullException("EstadosAccionRepository");
            _EstadosAccionRepository = EstadosAccionRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public EstadosAccion NewEntity()
         {
            return new EstadosAccion();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(EstadosAccion entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _EstadosAccionRepository.UnitOfWork;
            _EstadosAccionRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(EstadosAccion entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _EstadosAccionRepository.UnitOfWork;
            _EstadosAccionRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(EstadosAccion entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _EstadosAccionRepository.UnitOfWork;

            _EstadosAccionRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public EstadosAccion FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<EstadosAccion> specification = new DirectSpecification<EstadosAccion>(u => u.Estado == id.ToString());

            return _EstadosAccionRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public EstadosAccion FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<EstadosAccion> specification = new DirectSpecification<EstadosAccion>(u => u.Code == id);

            return _EstadosAccionRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<EstadosAccion> FindBySpec(bool isActive)
         {
            Specification<EstadosAccion> specification = new DirectSpecification<EstadosAccion>(u => u.Estado != null);
            return _EstadosAccionRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<EstadosAccion> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<EstadosAccion> onlyEnabledSpec = new DirectSpecification<EstadosAccion>(u => u.Estado != null);

            return _EstadosAccionRepository.GetPagedElements(pageIndex, pageCount, u => u.Estado, onlyEnabledSpec, true).ToList();
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

            if (_EstadosAccionRepository != null)
            {
                _EstadosAccionRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public EstadosAccion GetByEstado(string estado)
        {
            Specification<EstadosAccion> specification = new DirectSpecification<EstadosAccion>(u => u.Estado == estado);
            return _EstadosAccionRepository.GetEntityBySpec(specification);
        }
    }
}
    