using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfTiposContratoManagementServices : ISfTiposContratoManagementServices
    {

         #region Fields
         readonly ITiposContratoRepository _TiposContratoRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTiposContratoManagementServices( ITiposContratoRepository TiposContratoRepository)
         {
            if (TiposContratoRepository == null)
                throw new ArgumentNullException("TiposContratoRepository");
            _TiposContratoRepository = TiposContratoRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TiposContrato NewEntity()
         {
            return new TiposContrato();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TiposContrato entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TiposContratoRepository.UnitOfWork;
            _TiposContratoRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TiposContrato entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TiposContratoRepository.UnitOfWork;
            _TiposContratoRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TiposContrato entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TiposContratoRepository.UnitOfWork;

            _TiposContratoRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TiposContrato FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TiposContrato> specification = new DirectSpecification<TiposContrato>(u => u.IdTipoContrato == id.ToString());

            return _TiposContratoRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public TiposContrato FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TiposContrato> specification = new DirectSpecification<TiposContrato>(u => u.Code == id);

            return _TiposContratoRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TiposContrato> FindBySpec(bool isActive)
         {
             Specification<TiposContrato> specification = new DirectSpecification<TiposContrato>(u => u.IdTipoContrato != null);
            return _TiposContratoRepository.GetBySpec(specification).ToList();
         }

        public TiposContrato GetById(string id)
        {
            Specification<TiposContrato> specification = new DirectSpecification<TiposContrato>(u => u.IdTipoContrato == id);
            return _TiposContratoRepository.GetCompleteEntity(specification);
        }

        public int CountByPaged()
        {
            Specification<TiposContrato> onlyEnabledSpec = new DirectSpecification<TiposContrato>(u => true);

            return _TiposContratoRepository.GetBySpec(onlyEnabledSpec).Count();
        }

        /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TiposContrato> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TiposContrato> onlyEnabledSpec = new DirectSpecification<TiposContrato>(u => u.IdTipoContrato != null);

            return _TiposContratoRepository.GetPagedElements(pageIndex, pageCount, u => u.Descripcion, onlyEnabledSpec, true).ToList();
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

            if (_TiposContratoRepository != null)
            {
                _TiposContratoRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    