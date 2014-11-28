using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfNovedadesContratoManagementServices : ISfNovedadesContratoManagementServices
    {

         #region Fields
         readonly INovedadesContratoRepository _NovedadesContratoRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfNovedadesContratoManagementServices( INovedadesContratoRepository NovedadesContratoRepository)
         {
            if (NovedadesContratoRepository == null)
                throw new ArgumentNullException("NovedadesContratoRepository");
            _NovedadesContratoRepository = NovedadesContratoRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public NovedadesContrato NewEntity()
         {
            return new NovedadesContrato();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(NovedadesContrato entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _NovedadesContratoRepository.UnitOfWork;
            _NovedadesContratoRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(NovedadesContrato entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _NovedadesContratoRepository.UnitOfWork;
            _NovedadesContratoRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(NovedadesContrato entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _NovedadesContratoRepository.UnitOfWork;

            _NovedadesContratoRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public NovedadesContrato FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<NovedadesContrato> specification = new DirectSpecification<NovedadesContrato>(u => u.IdNovedad == id);

            return _NovedadesContratoRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public NovedadesContrato FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<NovedadesContrato> specification = new DirectSpecification<NovedadesContrato>(u => u.Code == id);

            return _NovedadesContratoRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<NovedadesContrato> FindBySpec(bool isActive)
         {
             Specification<NovedadesContrato> specification = new DirectSpecification<NovedadesContrato>(u => u.IdNovedad != null);
            return _NovedadesContratoRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<NovedadesContrato> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<NovedadesContrato> onlyEnabledSpec = new DirectSpecification<NovedadesContrato>(u => u.IdNovedad != null);

            return _NovedadesContratoRepository.GetPagedElements(pageIndex, pageCount, u => u.IdNovedad, onlyEnabledSpec, true).ToList();
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

            if (_NovedadesContratoRepository != null)
            {
                _NovedadesContratoRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public List<NovedadesContrato> GetNovedadesByContrato(int idContrato)
        {
            Specification<NovedadesContrato> specification = new DirectSpecification<NovedadesContrato>(u => u.IdContrato == idContrato);
            return _NovedadesContratoRepository.GetCompleteEntityList(specification);
        }
    }
}
    