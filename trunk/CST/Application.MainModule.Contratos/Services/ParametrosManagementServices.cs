using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfParametrosManagementServices : ISfParametrosManagementServices
    {

         #region Fields
         readonly IParametrosRepository _ParametrosRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfParametrosManagementServices( IParametrosRepository ParametrosRepository)
         {
            if (ParametrosRepository == null)
                throw new ArgumentNullException("ParametrosRepository");
            _ParametrosRepository = ParametrosRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public Parametros NewEntity()
         {
            return new Parametros();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(Parametros entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _ParametrosRepository.UnitOfWork;
            _ParametrosRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(Parametros entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _ParametrosRepository.UnitOfWork;
            _ParametrosRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(Parametros entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _ParametrosRepository.UnitOfWork;

            _ParametrosRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public Parametros FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<Parametros> specification = new DirectSpecification<Parametros>(u => u.IdParametro == id);

            return _ParametrosRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public Parametros FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<Parametros> specification = new DirectSpecification<Parametros>(u => u.Code == id);

            return _ParametrosRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<Parametros> FindBySpec(bool isActive)
         {
             Specification<Parametros> specification = new DirectSpecification<Parametros>(u => u.IdParametro != null);
            return _ParametrosRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<Parametros> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<Parametros> onlyEnabledSpec = new DirectSpecification<Parametros>(u => u.IdParametro != null);

            return _ParametrosRepository.GetPagedElements(pageIndex, pageCount, u => u.IdParametro, onlyEnabledSpec, true).ToList();
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

            if (_ParametrosRepository != null)
            {
                _ParametrosRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    