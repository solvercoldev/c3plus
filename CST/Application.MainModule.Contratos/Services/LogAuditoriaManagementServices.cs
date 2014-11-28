using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfLogAuditoriaManagementServices : ISfLogAuditoriaManagementServices
    {

         #region Fields
         readonly ILogAuditoriaRepository _LogAuditoriaRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfLogAuditoriaManagementServices( ILogAuditoriaRepository LogAuditoriaRepository)
         {
            if (LogAuditoriaRepository == null)
                throw new ArgumentNullException("LogAuditoriaRepository");
            _LogAuditoriaRepository = LogAuditoriaRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public LogAuditoria NewEntity()
         {
            return new LogAuditoria();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(LogAuditoria entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _LogAuditoriaRepository.UnitOfWork;
            _LogAuditoriaRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(LogAuditoria entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _LogAuditoriaRepository.UnitOfWork;
            _LogAuditoriaRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(LogAuditoria entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _LogAuditoriaRepository.UnitOfWork;

            _LogAuditoriaRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public LogAuditoria FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<LogAuditoria> specification = new DirectSpecification<LogAuditoria>(u => u.IdAuditoria == id);

            return _LogAuditoriaRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public LogAuditoria FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<LogAuditoria> specification = new DirectSpecification<LogAuditoria>(u => u.Code == id);

            return _LogAuditoriaRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<LogAuditoria> FindBySpec(bool isActive)
         {
             Specification<LogAuditoria> specification = new DirectSpecification<LogAuditoria>(u => u.IdAuditoria != null);
            return _LogAuditoriaRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<LogAuditoria> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<LogAuditoria> onlyEnabledSpec = new DirectSpecification<LogAuditoria>(u => u.IdAuditoria != null);

            return _LogAuditoriaRepository.GetPagedElements(pageIndex, pageCount, u => u.FechaHora, onlyEnabledSpec, true).ToList();
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

            if (_LogAuditoriaRepository != null)
            {
                _LogAuditoriaRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    