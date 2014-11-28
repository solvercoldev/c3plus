using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfFasesManagementServices : ISfFasesManagementServices
    {

         #region Fields
         readonly IFasesRepository _FasesRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfFasesManagementServices( IFasesRepository FasesRepository)
         {
            if (FasesRepository == null)
                throw new ArgumentNullException("FasesRepository");
            _FasesRepository = FasesRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public Fases NewEntity()
         {
            return new Fases();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(Fases entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _FasesRepository.UnitOfWork;
            _FasesRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(Fases entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _FasesRepository.UnitOfWork;
            _FasesRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(Fases entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _FasesRepository.UnitOfWork;

            _FasesRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public Fases FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<Fases> specification = new DirectSpecification<Fases>(u => u.IdFase == id);

            return _FasesRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public Fases FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<Fases> specification = new DirectSpecification<Fases>(u => u.Code == id);

            return _FasesRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<Fases> FindBySpec(bool isActive)
         {
             Specification<Fases> specification = new DirectSpecification<Fases>(u => u.IdFase != null);
            return _FasesRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<Fases> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<Fases> onlyEnabledSpec = new DirectSpecification<Fases>(u => u.IdFase != null);

            return _FasesRepository.GetPagedElements(pageIndex, pageCount, u => u.IdFase, onlyEnabledSpec, true).ToList();
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

            if (_FasesRepository != null)
            {
                _FasesRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public List<Fases> GetFasesByContrato(int idContrato)
        {
            Specification<Fases> specification = new DirectSpecification<Fases>(u => u.IdContrato == idContrato && u.IsActive);
            return _FasesRepository.GetBySpec(specification).ToList();
        }
    }
}
    