using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfDocumentosAnexoContratoManagementServices : ISfDocumentosAnexoContratoManagementServices
    {

         #region Fields
         readonly IDocumentosAnexoContratoRepository _DocumentosAnexoContratoRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfDocumentosAnexoContratoManagementServices( IDocumentosAnexoContratoRepository DocumentosAnexoContratoRepository)
         {
            if (DocumentosAnexoContratoRepository == null)
                throw new ArgumentNullException("DocumentosAnexoContratoRepository");
            _DocumentosAnexoContratoRepository = DocumentosAnexoContratoRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public DocumentosAnexoContrato NewEntity()
         {
            return new DocumentosAnexoContrato();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(DocumentosAnexoContrato entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _DocumentosAnexoContratoRepository.UnitOfWork;
            _DocumentosAnexoContratoRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(DocumentosAnexoContrato entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _DocumentosAnexoContratoRepository.UnitOfWork;
            _DocumentosAnexoContratoRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(DocumentosAnexoContrato entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _DocumentosAnexoContratoRepository.UnitOfWork;

            _DocumentosAnexoContratoRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public DocumentosAnexoContrato FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<DocumentosAnexoContrato> specification = new DirectSpecification<DocumentosAnexoContrato>(u => u.IdDocumentoContrato == Guid.NewGuid());

            return _DocumentosAnexoContratoRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public DocumentosAnexoContrato FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<DocumentosAnexoContrato> specification = new DirectSpecification<DocumentosAnexoContrato>(u => u.Code == id);

            return _DocumentosAnexoContratoRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<DocumentosAnexoContrato> FindBySpec(bool isActive)
         {
            Specification<DocumentosAnexoContrato> specification = new DirectSpecification<DocumentosAnexoContrato>(u => u.IsActive == isActive);
            return _DocumentosAnexoContratoRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<DocumentosAnexoContrato> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<DocumentosAnexoContrato> onlyEnabledSpec = new DirectSpecification<DocumentosAnexoContrato>(u => u.IsActive);

            return _DocumentosAnexoContratoRepository.GetPagedElements(pageIndex, pageCount, u => u.CreateOn, onlyEnabledSpec, true).ToList();
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

            if (_DocumentosAnexoContratoRepository != null)
            {
                _DocumentosAnexoContratoRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public List<DocumentosAnexoContrato> GetAnexosByContratoCategoria(int idContrato, string categoria)
        {
            Specification<DocumentosAnexoContrato> specification = new DirectSpecification<DocumentosAnexoContrato>(u => u.IdContrato == idContrato);

            if (!string.IsNullOrEmpty(categoria))
            {
                specification &= new DirectSpecification<DocumentosAnexoContrato>(u => u.Categoria == categoria);
            }

            return _DocumentosAnexoContratoRepository.GetCompleteEntityList(specification);
        }

        public DocumentosAnexoContrato GetById(Guid id)
        {
            Specification<DocumentosAnexoContrato> specification = new DirectSpecification<DocumentosAnexoContrato>(u => u.IdDocumentoContrato == id);
            
            return _DocumentosAnexoContratoRepository.GetCompleteEntity(specification);
        }
    }
}