using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfContratosManagementServices : ISfContratosManagementServices
    {

         #region Fields
         readonly IContratosRepository _ContratosRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfContratosManagementServices( IContratosRepository ContratosRepository)
         {
            if (ContratosRepository == null)
                throw new ArgumentNullException("ContratosRepository");
            _ContratosRepository = ContratosRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public Domain.MainModules.Entities.Contratos NewEntity()
         {
             return new Domain.MainModules.Entities.Contratos();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(Domain.MainModules.Entities.Contratos entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _ContratosRepository.UnitOfWork;
            _ContratosRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(Domain.MainModules.Entities.Contratos entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _ContratosRepository.UnitOfWork;
            _ContratosRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(Domain.MainModules.Entities.Contratos entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _ContratosRepository.UnitOfWork;

            _ContratosRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public Domain.MainModules.Entities.Contratos FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<Domain.MainModules.Entities.Contratos> specification = new DirectSpecification<Domain.MainModules.Entities.Contratos>(u => u.IdContrato == id);

            return _ContratosRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public Contratos FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<Contratos> specification = new DirectSpecification<Contratos>(u => u.Code == id);

            return _ContratosRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<Domain.MainModules.Entities.Contratos> FindBySpec(bool isActive)
         {
             Specification<Domain.MainModules.Entities.Contratos> specification = new DirectSpecification<Domain.MainModules.Entities.Contratos>(u => u.IdContrato != null);
            return _ContratosRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<Domain.MainModules.Entities.Contratos> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<Domain.MainModules.Entities.Contratos> onlyEnabledSpec = new DirectSpecification<Domain.MainModules.Entities.Contratos>(u => u.IdContrato != null);

            return _ContratosRepository.GetPagedElements(pageIndex, pageCount, u => u.NumeroContrato, onlyEnabledSpec, true).ToList();
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

            if (_ContratosRepository != null)
            {
                _ContratosRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public Domain.MainModules.Entities.Contratos GetContratoWithNavsById(int idContrato)
        {
            Specification<Domain.MainModules.Entities.Contratos> specification = new DirectSpecification<Domain.MainModules.Entities.Contratos>(u => u.IdContrato == idContrato);
            return _ContratosRepository.GetCompleteEntity(specification);
        }


        public List<Domain.MainModules.Entities.Contratos> GetContratoWithNavsByFilter(string idEmpresa, DateTime fechaInicioFirma, DateTime fechaFirmaFin)
        {
            Specification<Domain.MainModules.Entities.Contratos> specification = new DirectSpecification<Domain.MainModules.Entities.Contratos>(u => u.FechaFirma >= fechaInicioFirma
                                                                                                                                                     && u.FechaFirma <= fechaFirmaFin);

            if (!string.IsNullOrEmpty(idEmpresa))
            {
                specification &= new DirectSpecification<Domain.MainModules.Entities.Contratos>(u => u.IdEmpresa == idEmpresa);
            }

            return _ContratosRepository.GetCompleteEntityList(specification);
        }

        public List<Domain.MainModules.Entities.Contratos> GetContratoWithNavsByFilter(string idBloque, string estado, string fechaInicio)
        {
            Specification<Domain.MainModules.Entities.Contratos> specification = new DirectSpecification<Domain.MainModules.Entities.Contratos>(u => u.IsActive);

            if (!string.IsNullOrEmpty(idBloque))
            {
                specification &= new DirectSpecification<Domain.MainModules.Entities.Contratos>(u => u.IdBloque == idBloque);
            }

            if (!string.IsNullOrEmpty(estado))
            {
                specification &= new DirectSpecification<Domain.MainModules.Entities.Contratos>(u => u.Estado == estado);
            }

            if (!string.IsNullOrEmpty(fechaInicio))
            {
                var fecha = Convert.ToDateTime(fechaInicio);

                specification &= new DirectSpecification<Domain.MainModules.Entities.Contratos>(u => u.FechaInicio >= fecha);
            }

            return _ContratosRepository.GetCompleteEntityList(specification);
        }


        public bool ExistsContratoByNumero(string numero)
        {
            numero = numero.Trim();
            Specification<Domain.MainModules.Entities.Contratos> specification = new DirectSpecification<Domain.MainModules.Entities.Contratos>(u => u.IsActive && u.NumeroContrato == numero);

            var contrato = _ContratosRepository.GetEntityBySpec(specification);

            return contrato != null;

        }
    }
}
    