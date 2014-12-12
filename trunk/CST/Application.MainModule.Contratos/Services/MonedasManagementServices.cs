using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfMonedasManagementServices : ISfMonedasManagementServices
    {

         #region Fields
         readonly IMonedasRepository _MonedasRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfMonedasManagementServices( IMonedasRepository MonedasRepository)
         {
            if (MonedasRepository == null)
                throw new ArgumentNullException("MonedasRepository");
            _MonedasRepository = MonedasRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public Monedas NewEntity()
         {
            return new Monedas();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(Monedas entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _MonedasRepository.UnitOfWork;
            _MonedasRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(Monedas entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _MonedasRepository.UnitOfWork;
            _MonedasRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(Monedas entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _MonedasRepository.UnitOfWork;

            _MonedasRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public Monedas FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<Monedas> specification = new DirectSpecification<Monedas>(u => u.IdMoneda == id.ToString());

            return _MonedasRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public Monedas FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<Monedas> specification = new DirectSpecification<Monedas>(u => u.Code == id);

            return _MonedasRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<Monedas> FindBySpec(bool isActive)
         {
            Specification<Monedas> specification = new DirectSpecification<Monedas>(u => u.IdMoneda != null);
            return _MonedasRepository.GetBySpec(specification).ToList();
         }

        public Monedas GetById(string id)
        {
            Specification<Monedas> specification = new DirectSpecification<Monedas>(u => u.IdMoneda == id);

            return _MonedasRepository.GetEntityBySpec(specification);
        }

        /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<Monedas> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<Monedas> onlyEnabledSpec = new DirectSpecification<Monedas>(u => u.IdMoneda != null);

            return _MonedasRepository.GetPagedElements(pageIndex, pageCount, u => u.Nombre, onlyEnabledSpec, true).ToList();
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

            if (_MonedasRepository != null)
            {
                _MonedasRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    