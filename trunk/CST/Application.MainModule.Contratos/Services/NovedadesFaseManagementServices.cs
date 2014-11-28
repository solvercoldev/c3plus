using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfNovedadesFaseManagementServices : ISfNovedadesFaseManagementServices
    {

         #region Fields
         readonly INovedadesFaseRepository _NovedadesFaseRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfNovedadesFaseManagementServices( INovedadesFaseRepository NovedadesFaseRepository)
         {
            if (NovedadesFaseRepository == null)
                throw new ArgumentNullException("NovedadesFaseRepository");
            _NovedadesFaseRepository = NovedadesFaseRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public NovedadesFase NewEntity()
         {
            return new NovedadesFase();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(NovedadesFase entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _NovedadesFaseRepository.UnitOfWork;
            _NovedadesFaseRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(NovedadesFase entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _NovedadesFaseRepository.UnitOfWork;
            _NovedadesFaseRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(NovedadesFase entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _NovedadesFaseRepository.UnitOfWork;

            _NovedadesFaseRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public NovedadesFase FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<NovedadesFase> specification = new DirectSpecification<NovedadesFase>(u => u.IdNovedad == id);

            return _NovedadesFaseRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public NovedadesFase FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<NovedadesFase> specification = new DirectSpecification<NovedadesFase>(u => u.Code == id);

            return _NovedadesFaseRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<NovedadesFase> FindBySpec(bool isActive)
         {
            Specification<NovedadesFase> specification = new DirectSpecification<NovedadesFase>(u => u.IdNovedad != null);
            return _NovedadesFaseRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<NovedadesFase> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<NovedadesFase> onlyEnabledSpec = new DirectSpecification<NovedadesFase>(u => u.IdNovedad != null);

            return _NovedadesFaseRepository.GetPagedElements(pageIndex, pageCount, u => u.IdNovedad, onlyEnabledSpec, true).ToList();
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

            if (_NovedadesFaseRepository != null)
            {
                _NovedadesFaseRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public List<NovedadesFase> GetByContratoFase(int idContrato, int idFase)
        {
            Specification<NovedadesFase> specification = new DirectSpecification<NovedadesFase>(u => u.Fases.IdContrato == idContrato);

            if (idFase != 0)
            {
                specification &= new DirectSpecification<NovedadesFase>(u => u.IdFase == idFase);
            }

            return _NovedadesFaseRepository.GetCompleteEntityList(specification);
        }
    }
}
    