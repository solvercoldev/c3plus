using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfDocumentosRadicadoManagementServices : ISfDocumentosRadicadoManagementServices
    {

         #region Fields
         readonly IDocumentosRadicadoRepository _DocumentosRadicadoRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfDocumentosRadicadoManagementServices( IDocumentosRadicadoRepository DocumentosRadicadoRepository)
         {
            if (DocumentosRadicadoRepository == null)
                throw new ArgumentNullException("DocumentosRadicadoRepository");
            _DocumentosRadicadoRepository = DocumentosRadicadoRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public DocumentosRadicado NewEntity()
         {
            return new DocumentosRadicado();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(DocumentosRadicado entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _DocumentosRadicadoRepository.UnitOfWork;
            _DocumentosRadicadoRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(DocumentosRadicado entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _DocumentosRadicadoRepository.UnitOfWork;
            _DocumentosRadicadoRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(DocumentosRadicado entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _DocumentosRadicadoRepository.UnitOfWork;

            _DocumentosRadicadoRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public DocumentosRadicado FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<DocumentosRadicado> specification = new DirectSpecification<DocumentosRadicado>(u => u.IdDocumentoRadicado == id.ToString());

            return _DocumentosRadicadoRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public DocumentosRadicado FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<DocumentosRadicado> specification = new DirectSpecification<DocumentosRadicado>(u => u.Code == id);

            return _DocumentosRadicadoRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<DocumentosRadicado> FindBySpec(bool isActive)
         {
             Specification<DocumentosRadicado> specification = new DirectSpecification<DocumentosRadicado>(u => u.IdRadicado != null);
            return _DocumentosRadicadoRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<DocumentosRadicado> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<DocumentosRadicado> onlyEnabledSpec = new DirectSpecification<DocumentosRadicado>(u => u.IdRadicado != null);

            return _DocumentosRadicadoRepository.GetPagedElements(pageIndex, pageCount, u => u.Descripcion, onlyEnabledSpec, true).ToList();
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

            if (_DocumentosRadicadoRepository != null)
            {
                _DocumentosRadicadoRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    