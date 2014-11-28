using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfAlarmasCompromisoManagementServices : ISfAlarmasCompromisoManagementServices
    {

         #region Fields
         readonly IAlarmasCompromisoRepository _AlarmasCompromisoRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfAlarmasCompromisoManagementServices( IAlarmasCompromisoRepository AlarmasCompromisoRepository)
         {
            if (AlarmasCompromisoRepository == null)
                throw new ArgumentNullException("AlarmasCompromisoRepository");
            _AlarmasCompromisoRepository = AlarmasCompromisoRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public AlarmasCompromiso NewEntity()
         {
            return new AlarmasCompromiso();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(AlarmasCompromiso entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _AlarmasCompromisoRepository.UnitOfWork;
            _AlarmasCompromisoRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(AlarmasCompromiso entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _AlarmasCompromisoRepository.UnitOfWork;
            _AlarmasCompromisoRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(AlarmasCompromiso entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _AlarmasCompromisoRepository.UnitOfWork;

            _AlarmasCompromisoRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public AlarmasCompromiso FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<AlarmasCompromiso> specification = new DirectSpecification<AlarmasCompromiso>(u => u.IdAlarmaCompromiso == id);

            return _AlarmasCompromisoRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public AlarmasCompromiso FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<AlarmasCompromiso> specification = new DirectSpecification<AlarmasCompromiso>(u => u.Code == id);

            return _AlarmasCompromisoRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<AlarmasCompromiso> FindBySpec(bool isActive)
         {
            Specification<AlarmasCompromiso> specification = new DirectSpecification<AlarmasCompromiso>(u => u.Activa == isActive);
            return _AlarmasCompromisoRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<AlarmasCompromiso> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<AlarmasCompromiso> onlyEnabledSpec = new DirectSpecification<AlarmasCompromiso>(u => u.Activa);

            return _AlarmasCompromisoRepository.GetPagedElements(pageIndex, pageCount, u => u.Activa, onlyEnabledSpec, true).ToList();
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

            if (_AlarmasCompromisoRepository != null)
            {
                _AlarmasCompromisoRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    