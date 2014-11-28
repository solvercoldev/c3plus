using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfRadicadosManagementServices : ISfRadicadosManagementServices
    {

         #region Fields
         readonly IRadicadosRepository _RadicadosRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfRadicadosManagementServices( IRadicadosRepository RadicadosRepository)
         {
            if (RadicadosRepository == null)
                throw new ArgumentNullException("RadicadosRepository");
            _RadicadosRepository = RadicadosRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public Radicados NewEntity()
         {
            return new Radicados();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(Radicados entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _RadicadosRepository.UnitOfWork;
            _RadicadosRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(Radicados entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _RadicadosRepository.UnitOfWork;
            _RadicadosRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(Radicados entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _RadicadosRepository.UnitOfWork;

            _RadicadosRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public Radicados FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<Radicados> specification = new DirectSpecification<Radicados>(u => u.IdRadicado == id);

            return _RadicadosRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public Radicados FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<Radicados> specification = new DirectSpecification<Radicados>(u => u.Code == id);

            return _RadicadosRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<Radicados> FindBySpec(bool isActive)
         {
             Specification<Radicados> specification = new DirectSpecification<Radicados>(u => u.IdRadicado != null);
            return _RadicadosRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<Radicados> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<Radicados> onlyEnabledSpec = new DirectSpecification<Radicados>(u => u.IdRadicado != null);

            return _RadicadosRepository.GetPagedElements(pageIndex, pageCount, u => u.Numero, onlyEnabledSpec, true).ToList();
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

            if (_RadicadosRepository != null)
            {
                _RadicadosRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    