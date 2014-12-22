using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfDependenciasManagementServices : ISfDependenciasManagementServices
    {

         #region Fields
         readonly IDependenciasRepository _DependenciasRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfDependenciasManagementServices( IDependenciasRepository DependenciasRepository)
         {
            if (DependenciasRepository == null)
                throw new ArgumentNullException("DependenciasRepository");
            _DependenciasRepository = DependenciasRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public Dependencias NewEntity()
         {
            return new Dependencias();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(Dependencias entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _DependenciasRepository.UnitOfWork;
            _DependenciasRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(Dependencias entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _DependenciasRepository.UnitOfWork;
            _DependenciasRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(Dependencias entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _DependenciasRepository.UnitOfWork;

            _DependenciasRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public Dependencias FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<Dependencias> specification = new DirectSpecification<Dependencias>(u => u.IdDependencia == id.ToString());

            return _DependenciasRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public Dependencias FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<Dependencias> specification = new DirectSpecification<Dependencias>(u => u.Code == id);

            return _DependenciasRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<Dependencias> FindBySpec(bool isActive)
         {
             Specification<Dependencias> specification = new DirectSpecification<Dependencias>(u => u.IdDependencia != null);
            return _DependenciasRepository.GetBySpec(specification).ToList();
         }

        public Dependencias GetById(string id)
        {
            Specification<Dependencias> specification = new DirectSpecification<Dependencias>(u => u.IdDependencia == id);
            return _DependenciasRepository.GetCompleteEntity(specification);
        }

        public int CountByPaged()
        {
            Specification<Dependencias> onlyEnabledSpec = new DirectSpecification<Dependencias>(u => true);

            return _DependenciasRepository.GetBySpec(onlyEnabledSpec).Count();
        }

        /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<Dependencias> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<Dependencias> onlyEnabledSpec = new DirectSpecification<Dependencias>(u => u.IdDependencia != null);

            return _DependenciasRepository.GetPagedElements(pageIndex, pageCount, u => u.Descripcion, onlyEnabledSpec, true).ToList();
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

            if (_DependenciasRepository != null)
            {
                _DependenciasRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    