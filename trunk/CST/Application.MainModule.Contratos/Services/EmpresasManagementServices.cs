using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfEmpresasManagementServices : ISfEmpresasManagementServices
    {

         #region Fields
         readonly IEmpresasRepository _EmpresasRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfEmpresasManagementServices( IEmpresasRepository EmpresasRepository)
         {
            if (EmpresasRepository == null)
                throw new ArgumentNullException("EmpresasRepository");
            _EmpresasRepository = EmpresasRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public Empresas NewEntity()
         {
            return new Empresas();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(Empresas entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _EmpresasRepository.UnitOfWork;
            _EmpresasRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(Empresas entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _EmpresasRepository.UnitOfWork;
            _EmpresasRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(Empresas entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _EmpresasRepository.UnitOfWork;

            _EmpresasRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public Empresas FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<Empresas> specification = new DirectSpecification<Empresas>(u => u.Nit != null);

            return _EmpresasRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public Empresas FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<Empresas> specification = new DirectSpecification<Empresas>(u => u.Code == id);

            return _EmpresasRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<Empresas> FindBySpec(bool isActive)
         {
             Specification<Empresas> specification = new DirectSpecification<Empresas>(u => u.Nit != null);
            return _EmpresasRepository.GetBySpec(specification).ToList();
         }

        public Empresas GetById(string id)
        {
             Specification<Empresas> specification = new DirectSpecification<Empresas>(u => u.Nit == id);
             return _EmpresasRepository.GetCompleteEntity(specification);
        }

        /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<Empresas> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<Empresas> onlyEnabledSpec = new DirectSpecification<Empresas>(u => u.Nit != null);

            return _EmpresasRepository.GetPagedElements(pageIndex, pageCount, u => u.RazonSocial, onlyEnabledSpec, true).ToList();
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

            if (_EmpresasRepository != null)
            {
                _EmpresasRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    