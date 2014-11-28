using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfPagosObligacionesManagementServices : ISfPagosObligacionesManagementServices
    {

         #region Fields
         readonly IPagosObligacionesRepository _PagosObligacionesRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfPagosObligacionesManagementServices( IPagosObligacionesRepository PagosObligacionesRepository)
         {
            if (PagosObligacionesRepository == null)
                throw new ArgumentNullException("PagosObligacionesRepository");
            _PagosObligacionesRepository = PagosObligacionesRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public PagosObligaciones NewEntity()
         {
            return new PagosObligaciones();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(PagosObligaciones entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _PagosObligacionesRepository.UnitOfWork;
            _PagosObligacionesRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(PagosObligaciones entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _PagosObligacionesRepository.UnitOfWork;
            _PagosObligacionesRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(PagosObligaciones entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _PagosObligacionesRepository.UnitOfWork;

            _PagosObligacionesRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public PagosObligaciones FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<PagosObligaciones> specification = new DirectSpecification<PagosObligaciones>(u => u.IdPagoObligacion == id);

            return _PagosObligacionesRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public PagosObligaciones FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<PagosObligaciones> specification = new DirectSpecification<PagosObligaciones>(u => u.Code == id);

            return _PagosObligacionesRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<PagosObligaciones> FindBySpec(bool isActive)
         {
            Specification<PagosObligaciones> specification = new DirectSpecification<PagosObligaciones>(u => u.IdPagoObligacion != null);
            return _PagosObligacionesRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<PagosObligaciones> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<PagosObligaciones> onlyEnabledSpec = new DirectSpecification<PagosObligaciones>(u => u.IdPagoObligacion != null);

            return _PagosObligacionesRepository.GetPagedElements(pageIndex, pageCount, u => u.IdPagoObligacion, onlyEnabledSpec, true).ToList();
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

            if (_PagosObligacionesRepository != null)
            {
                _PagosObligacionesRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public PagosObligaciones GetPagoByCompromiso(long idCompromiso)
        {
            Specification<PagosObligaciones> specification = new DirectSpecification<PagosObligaciones>(u => u.IdCompromiso == idCompromiso);

            return _PagosObligacionesRepository.GetEntityBySpec(specification);
        }
    }
}
    