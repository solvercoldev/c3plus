using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfEntregablesANHCompromisoManagementServices : ISfEntregablesANHCompromisoManagementServices
    {

         #region Fields
         readonly IEntregablesANHCompromisoRepository _EntregablesANHCompromisoRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfEntregablesANHCompromisoManagementServices( IEntregablesANHCompromisoRepository EntregablesANHCompromisoRepository)
         {
            if (EntregablesANHCompromisoRepository == null)
                throw new ArgumentNullException("EntregablesANHCompromisoRepository");
            _EntregablesANHCompromisoRepository = EntregablesANHCompromisoRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public EntregablesANHCompromiso NewEntity()
         {
            return new EntregablesANHCompromiso();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(EntregablesANHCompromiso entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _EntregablesANHCompromisoRepository.UnitOfWork;
            _EntregablesANHCompromisoRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(EntregablesANHCompromiso entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _EntregablesANHCompromisoRepository.UnitOfWork;
            _EntregablesANHCompromisoRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(EntregablesANHCompromiso entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _EntregablesANHCompromisoRepository.UnitOfWork;

            _EntregablesANHCompromisoRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public EntregablesANHCompromiso FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<EntregablesANHCompromiso> specification = new DirectSpecification<EntregablesANHCompromiso>(u => u.IdManualAnh == id.ToString());

            return _EntregablesANHCompromisoRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public EntregablesANHCompromiso FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<EntregablesANHCompromiso> specification = new DirectSpecification<EntregablesANHCompromiso>(u => u.Code == id);

            return _EntregablesANHCompromisoRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<EntregablesANHCompromiso> FindBySpec(bool isActive)
         {
             Specification<EntregablesANHCompromiso> specification = new DirectSpecification<EntregablesANHCompromiso>(u => u.IdCompromiso != null);
            return _EntregablesANHCompromisoRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<EntregablesANHCompromiso> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<EntregablesANHCompromiso> onlyEnabledSpec = new DirectSpecification<EntregablesANHCompromiso>(u => u.IdCompromiso != null);

            return _EntregablesANHCompromisoRepository.GetPagedElements(pageIndex, pageCount, u => u.IdCompromiso, onlyEnabledSpec, true).ToList();
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

            if (_EntregablesANHCompromisoRepository != null)
            {
                _EntregablesANHCompromisoRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    