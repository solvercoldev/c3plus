using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfTercerosManagementServices : ISfTercerosManagementServices
    {

         #region Fields
         readonly ITercerosRepository _TercerosRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTercerosManagementServices( ITercerosRepository TercerosRepository)
         {
            if (TercerosRepository == null)
                throw new ArgumentNullException("TercerosRepository");
            _TercerosRepository = TercerosRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public Terceros NewEntity()
         {
            return new Terceros();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(Terceros entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TercerosRepository.UnitOfWork;
            _TercerosRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(Terceros entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TercerosRepository.UnitOfWork;
            _TercerosRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(Terceros entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TercerosRepository.UnitOfWork;

            _TercerosRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public Terceros FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<Terceros> specification = new DirectSpecification<Terceros>(u => u.IdTercero == id.ToString());

            return _TercerosRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public Terceros FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<Terceros> specification = new DirectSpecification<Terceros>(u => u.Code == id);

            return _TercerosRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<Terceros> FindBySpec(bool isActive)
         {
             Specification<Terceros> specification = new DirectSpecification<Terceros>(u => u.IdTercero != null);
            return _TercerosRepository.GetBySpec(specification).ToList();
         }

        public Terceros GetById(string id)
        {
            Specification<Terceros> specification = new DirectSpecification<Terceros>(u => u.IdTercero == id);

            return _TercerosRepository.GetEntityBySpec(specification);
        }

        public int CountByPaged()
        {
            Specification<Terceros> onlyEnabledSpec = new DirectSpecification<Terceros>(u => true);

            return _TercerosRepository.GetBySpec(onlyEnabledSpec).Count();
        }

        public object FindByIdString(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<Terceros> specification = new DirectSpecification<Terceros>(u => u.IdTercero == id);

            return _TercerosRepository.GetEntityBySpec(specification);
        }

        /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<Terceros> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<Terceros> onlyEnabledSpec = new DirectSpecification<Terceros>(u => u.IdTercero != null);

            return _TercerosRepository.GetPagedElements(pageIndex, pageCount, u => u.Nombre, onlyEnabledSpec, true).ToList();
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

            if (_TercerosRepository != null)
            {
                _TercerosRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    