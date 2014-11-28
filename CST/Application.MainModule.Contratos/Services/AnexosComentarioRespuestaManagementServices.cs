using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfAnexosComentarioRespuestaManagementServices : ISfAnexosComentarioRespuestaManagementServices
    {

         #region Fields
         readonly IAnexosComentarioRespuestaRepository _AnexosComentarioRespuestaRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfAnexosComentarioRespuestaManagementServices( IAnexosComentarioRespuestaRepository AnexosComentarioRespuestaRepository)
         {
            if (AnexosComentarioRespuestaRepository == null)
                throw new ArgumentNullException("AnexosComentarioRespuestaRepository");
            _AnexosComentarioRespuestaRepository = AnexosComentarioRespuestaRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public AnexosComentarioRespuesta NewEntity()
         {
            return new AnexosComentarioRespuesta();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(AnexosComentarioRespuesta entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _AnexosComentarioRespuestaRepository.UnitOfWork;
            _AnexosComentarioRespuestaRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(AnexosComentarioRespuesta entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _AnexosComentarioRespuestaRepository.UnitOfWork;
            _AnexosComentarioRespuestaRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(AnexosComentarioRespuesta entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _AnexosComentarioRespuestaRepository.UnitOfWork;

            _AnexosComentarioRespuestaRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public AnexosComentarioRespuesta FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<AnexosComentarioRespuesta> specification = new DirectSpecification<AnexosComentarioRespuesta>(u => u.IdAnexoComentario == id);

            return _AnexosComentarioRespuestaRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public AnexosComentarioRespuesta FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<AnexosComentarioRespuesta> specification = new DirectSpecification<AnexosComentarioRespuesta>(u => u.Code == id);

            return _AnexosComentarioRespuestaRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<AnexosComentarioRespuesta> FindBySpec(bool isActive)
         {
            Specification<AnexosComentarioRespuesta> specification = new DirectSpecification<AnexosComentarioRespuesta>(u => u.IsActive == isActive);
            return _AnexosComentarioRespuestaRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<AnexosComentarioRespuesta> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<AnexosComentarioRespuesta> onlyEnabledSpec = new DirectSpecification<AnexosComentarioRespuesta>(u => u.IsActive);

            return _AnexosComentarioRespuestaRepository.GetPagedElements(pageIndex, pageCount, u => u.NombreArchivo, onlyEnabledSpec, true).ToList();
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

            if (_AnexosComentarioRespuestaRepository != null)
            {
                _AnexosComentarioRespuestaRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public List<AnexosComentarioRespuesta> GetByComentarioId(decimal idComentario)
        {
            Specification<AnexosComentarioRespuesta> spec = new DirectSpecification<AnexosComentarioRespuesta>(u => u.IdComentario == idComentario);

            return _AnexosComentarioRespuestaRepository.GetBySpec(spec).ToList();
        }

        public AnexosComentarioRespuesta GetById(decimal id)
        {
            Specification<AnexosComentarioRespuesta> spec = new DirectSpecification<AnexosComentarioRespuesta>(u => u.IdAnexoComentario == id);

            return _AnexosComentarioRespuestaRepository.GetEntityBySpec(spec);
        }
    }
}