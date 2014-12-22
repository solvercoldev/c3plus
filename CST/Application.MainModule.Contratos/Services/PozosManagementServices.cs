using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfPozosManagementServices : ISfPozosManagementServices
    {

         #region Fields
         readonly IPozosRepository _PozosRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfPozosManagementServices( IPozosRepository PozosRepository)
         {
            if (PozosRepository == null)
                throw new ArgumentNullException("PozosRepository");
            _PozosRepository = PozosRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public Pozos NewEntity()
         {
            return new Pozos();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(Pozos entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _PozosRepository.UnitOfWork;
            _PozosRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(Pozos entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _PozosRepository.UnitOfWork;
            _PozosRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(Pozos entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _PozosRepository.UnitOfWork;

            _PozosRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public Pozos FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<Pozos> specification = new DirectSpecification<Pozos>(u => u.IdPozo == id.ToString());

            return _PozosRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public Pozos FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<Pozos> specification = new DirectSpecification<Pozos>(u => u.Code == id);

            return _PozosRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<Pozos> FindBySpec(bool isActive)
         {
             Specification<Pozos> specification = new DirectSpecification<Pozos>(u => u.IdPozo != null);
            return _PozosRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<Pozos> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<Pozos> onlyEnabledSpec = new DirectSpecification<Pozos>(u => u.IdPozo != null);

            return _PozosRepository.GetPagedElements(pageIndex, pageCount, u => u.Descripcion, onlyEnabledSpec, true).ToList();
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

            if (_PozosRepository != null)
            {
                _PozosRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public List<Pozos> GetByBloque(string idBloque)
        {
            Specification<Pozos> specification = new DirectSpecification<Pozos>(u => u.Campos.IdBloque == idBloque);
            return _PozosRepository.GetBySpec(specification).ToList();
        }

        public Pozos GetById(string id)
        {
            Specification<Pozos> specification = new DirectSpecification<Pozos>(u => u.IdPozo == id);
            return _PozosRepository.GetCompleteEntity(specification);
        }

        public int CountByPaged()
        {
            Specification<Pozos> onlyEnabledSpec = new DirectSpecification<Pozos>(u => true);

            return _PozosRepository.GetBySpec(onlyEnabledSpec).Count();
        }
    }
}
    