using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfComentariosRespuestaManagementServices : ISfComentariosRespuestaManagementServices
    {

         #region Fields
         readonly IComentariosRespuestaRepository _ComentariosRespuestaRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfComentariosRespuestaManagementServices( IComentariosRespuestaRepository ComentariosRespuestaRepository)
         {
            if (ComentariosRespuestaRepository == null)
                throw new ArgumentNullException("ComentariosRespuestaRepository");
            _ComentariosRespuestaRepository = ComentariosRespuestaRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public ComentariosRespuesta NewEntity()
         {
            return new ComentariosRespuesta();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(ComentariosRespuesta entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _ComentariosRespuestaRepository.UnitOfWork;
            _ComentariosRespuestaRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(ComentariosRespuesta entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _ComentariosRespuestaRepository.UnitOfWork;
            _ComentariosRespuestaRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(ComentariosRespuesta entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _ComentariosRespuestaRepository.UnitOfWork;

            _ComentariosRespuestaRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public ComentariosRespuesta FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<ComentariosRespuesta> specification = new DirectSpecification<ComentariosRespuesta>(u => u.IdComentario == id);

            return _ComentariosRespuestaRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public ComentariosRespuesta FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<ComentariosRespuesta> specification = new DirectSpecification<ComentariosRespuesta>(u => u.Code == id);

            return _ComentariosRespuestaRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<ComentariosRespuesta> FindBySpec(bool isActive)
         {
            Specification<ComentariosRespuesta> specification = new DirectSpecification<ComentariosRespuesta>(u => u.IsActive == isActive);
            return _ComentariosRespuestaRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<ComentariosRespuesta> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<ComentariosRespuesta> onlyEnabledSpec = new DirectSpecification<ComentariosRespuesta>(u => u.IsActive);

            return _ComentariosRespuestaRepository.GetPagedElements(pageIndex, pageCount, u => u.CreateOn, onlyEnabledSpec, true).ToList();
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

            if (_ComentariosRespuestaRepository != null)
            {
                _ComentariosRespuestaRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public List<ComentariosRespuesta> GetByContrato(int idContrato)
        {
            Specification<ComentariosRespuesta> specification = new DirectSpecification<ComentariosRespuesta>(u => u.IdContrato == idContrato);
            return _ComentariosRespuestaRepository.GetCompleteListBySpec(specification);
        }

        public ComentariosRespuesta GetById(decimal id)
        {
            Specification<ComentariosRespuesta> spec = new DirectSpecification<ComentariosRespuesta>(u => u.IdComentario == id);

            return _ComentariosRespuestaRepository.GetCompleteEntityBySpec(spec);
        }

        public List<ComentariosRespuesta> GetByIdComentarioRelacionado(decimal idComentario)
        {
            Specification<ComentariosRespuesta> spec = new DirectSpecification<ComentariosRespuesta>(u => u.IdComentarioRelacionado == idComentario);

            return _ComentariosRespuestaRepository.GetCompleteListBySpec(spec);
        }
    }
}
    