using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfLogContratosManagementServices : ISfLogContratosManagementServices
    {

         #region Fields
         readonly ILogContratosRepository _LogContratosRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfLogContratosManagementServices( ILogContratosRepository LogContratosRepository)
         {
            if (LogContratosRepository == null)
                throw new ArgumentNullException("LogContratosRepository");
            _LogContratosRepository = LogContratosRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public LogContratos NewEntity()
         {
            return new LogContratos();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(LogContratos entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _LogContratosRepository.UnitOfWork;
            _LogContratosRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(LogContratos entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _LogContratosRepository.UnitOfWork;
            _LogContratosRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(LogContratos entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _LogContratosRepository.UnitOfWork;

            _LogContratosRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public LogContratos FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<LogContratos> specification = new DirectSpecification<LogContratos>(u => u.IdLog == Guid.Parse(id.ToString()));

            return _LogContratosRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public LogContratos FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<LogContratos> specification = new DirectSpecification<LogContratos>(u => u.Code == id);

            return _LogContratosRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<LogContratos> FindBySpec(bool isActive)
         {
            Specification<LogContratos> specification = new DirectSpecification<LogContratos>(u => u.IsActive == isActive);
            return _LogContratosRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<LogContratos> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<LogContratos> onlyEnabledSpec = new DirectSpecification<LogContratos>(u => u.IsActive);

            return _LogContratosRepository.GetPagedElements(pageIndex, pageCount, u => u.CreateOn, onlyEnabledSpec, true).ToList();
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

            if (_LogContratosRepository != null)
            {
                _LogContratosRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public List<LogContratos> GetByIdContrato(int idContrato)
        {
            Specification<LogContratos> specification = new DirectSpecification<LogContratos>(u => u.IdContrato == idContrato);
            return _LogContratosRepository.GetBySpec(specification).ToList();
        }
    }
}
    