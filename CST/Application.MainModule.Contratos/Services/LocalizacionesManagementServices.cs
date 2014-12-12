using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfLocalizacionesManagementServices : ISfLocalizacionesManagementServices
    {

         #region Fields
         readonly ILocalizacionesRepository _LocalizacionesRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfLocalizacionesManagementServices( ILocalizacionesRepository LocalizacionesRepository)
         {
            if (LocalizacionesRepository == null)
                throw new ArgumentNullException("LocalizacionesRepository");
            _LocalizacionesRepository = LocalizacionesRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public Localizaciones NewEntity()
         {
            return new Localizaciones();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(Localizaciones entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _LocalizacionesRepository.UnitOfWork;
            _LocalizacionesRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(Localizaciones entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _LocalizacionesRepository.UnitOfWork;
            _LocalizacionesRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(Localizaciones entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _LocalizacionesRepository.UnitOfWork;

            _LocalizacionesRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public Localizaciones FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<Localizaciones> specification = new DirectSpecification<Localizaciones>(u => u.IdLocalizacion == id.ToString());

            return _LocalizacionesRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public Localizaciones FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<Localizaciones> specification = new DirectSpecification<Localizaciones>(u => u.Code == id);

            return _LocalizacionesRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<Localizaciones> FindBySpec(bool isActive)
         {
             Specification<Localizaciones> specification = new DirectSpecification<Localizaciones>(u => u.IdLocalizacion != null);
            return _LocalizacionesRepository.GetBySpec(specification).ToList();
         }

        public Localizaciones GetById(string id)
        {
            Specification<Localizaciones> specification = new DirectSpecification<Localizaciones>(u => u.IdLocalizacion == id);
            return _LocalizacionesRepository.GetCompleteEntity(specification);
        }

        /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<Localizaciones> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<Localizaciones> onlyEnabledSpec = new DirectSpecification<Localizaciones>(u => u.IdLocalizacion != null);

            return _LocalizacionesRepository.GetPagedElements(pageIndex, pageCount, u => u.Descripcion, onlyEnabledSpec, true).ToList();
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

            if (_LocalizacionesRepository != null)
            {
                _LocalizacionesRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    