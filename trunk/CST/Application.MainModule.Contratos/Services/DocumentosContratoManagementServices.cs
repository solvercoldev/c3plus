using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfDocumentosContratoManagementServices : ISfDocumentosContratoManagementServices
    {

         #region Fields
         readonly IDocumentosContratoRepository _DocumentosContratoRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfDocumentosContratoManagementServices( IDocumentosContratoRepository DocumentosContratoRepository)
         {
            if (DocumentosContratoRepository == null)
                throw new ArgumentNullException("DocumentosContratoRepository");
            _DocumentosContratoRepository = DocumentosContratoRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public DocumentosContrato NewEntity()
         {
            return new DocumentosContrato();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(DocumentosContrato entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _DocumentosContratoRepository.UnitOfWork;
            _DocumentosContratoRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(DocumentosContrato entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _DocumentosContratoRepository.UnitOfWork;
            _DocumentosContratoRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(DocumentosContrato entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _DocumentosContratoRepository.UnitOfWork;

            _DocumentosContratoRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public DocumentosContrato FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<DocumentosContrato> specification = new DirectSpecification<DocumentosContrato>(u => u.IdDocumentoContrato == id.ToString());

            return _DocumentosContratoRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public DocumentosContrato FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<DocumentosContrato> specification = new DirectSpecification<DocumentosContrato>(u => u.Code == id);

            return _DocumentosContratoRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<DocumentosContrato> FindBySpec(bool isActive)
         {
             Specification<DocumentosContrato> specification = new DirectSpecification<DocumentosContrato>(u => u.IdContrato != null);
            return _DocumentosContratoRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<DocumentosContrato> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<DocumentosContrato> onlyEnabledSpec = new DirectSpecification<DocumentosContrato>(u => u.IdContrato != null);

            return _DocumentosContratoRepository.GetPagedElements(pageIndex, pageCount, u => u.Nombre, onlyEnabledSpec, true).ToList();
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

            if (_DocumentosContratoRepository != null)
            {
                _DocumentosContratoRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    