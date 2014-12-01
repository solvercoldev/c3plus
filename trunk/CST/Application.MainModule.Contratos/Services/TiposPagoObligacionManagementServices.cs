using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfTiposPagoObligacionManagementServices : ISfTiposPagoObligacionManagementServices
    {

         #region Fields
         readonly ITiposPagoObligacionRepository _TiposPagoObligacionRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTiposPagoObligacionManagementServices( ITiposPagoObligacionRepository TiposPagoObligacionRepository)
         {
            if (TiposPagoObligacionRepository == null)
                throw new ArgumentNullException("TiposPagoObligacionRepository");
            _TiposPagoObligacionRepository = TiposPagoObligacionRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TiposPagoObligacion NewEntity()
         {
            return new TiposPagoObligacion();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TiposPagoObligacion entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TiposPagoObligacionRepository.UnitOfWork;
            _TiposPagoObligacionRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TiposPagoObligacion entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TiposPagoObligacionRepository.UnitOfWork;
            _TiposPagoObligacionRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TiposPagoObligacion entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TiposPagoObligacionRepository.UnitOfWork;

            _TiposPagoObligacionRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TiposPagoObligacion FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TiposPagoObligacion> specification = new DirectSpecification<TiposPagoObligacion>(u => u.IdTipoPagoObligacion == id.ToString());

            return _TiposPagoObligacionRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public TiposPagoObligacion FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TiposPagoObligacion> specification = new DirectSpecification<TiposPagoObligacion>(u => u.Code == id);

            return _TiposPagoObligacionRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TiposPagoObligacion> FindBySpec(bool isActive)
         {
             Specification<TiposPagoObligacion> specification = new DirectSpecification<TiposPagoObligacion>(u => u.IdTipoPagoObligacion != null);
            return _TiposPagoObligacionRepository.GetBySpec(specification).ToList();
         }

        public TiposPagoObligacion GetById(string id)
        {
            Specification<TiposPagoObligacion> specification = new DirectSpecification<TiposPagoObligacion>(u => u.IdTipoPagoObligacion == id);

            return _TiposPagoObligacionRepository.GetEntityBySpec(specification);
        }

        /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TiposPagoObligacion> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TiposPagoObligacion> onlyEnabledSpec = new DirectSpecification<TiposPagoObligacion>(u => u.IdTipoPagoObligacion != null);

            return _TiposPagoObligacionRepository.GetPagedElements(pageIndex, pageCount, u => u.Descripcion, onlyEnabledSpec, true).ToList();
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

            if (_TiposPagoObligacionRepository != null)
            {
                _TiposPagoObligacionRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    