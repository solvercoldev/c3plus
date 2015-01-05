using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfCompromisosManagementServices : ISfCompromisosManagementServices
    {

         #region Fields
         readonly ICompromisosRepository _CompromisosRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfCompromisosManagementServices( ICompromisosRepository CompromisosRepository)
         {
            if (CompromisosRepository == null)
                throw new ArgumentNullException("CompromisosRepository");
            _CompromisosRepository = CompromisosRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public Compromisos NewEntity()
         {
            return new Compromisos();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(Compromisos entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _CompromisosRepository.UnitOfWork;
            _CompromisosRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(Compromisos entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _CompromisosRepository.UnitOfWork;
            _CompromisosRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(Compromisos entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _CompromisosRepository.UnitOfWork;

            _CompromisosRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public Compromisos FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<Compromisos> specification = new DirectSpecification<Compromisos>(u => u.IdCompromiso == id);

            return _CompromisosRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public Compromisos FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<Compromisos> specification = new DirectSpecification<Compromisos>(u => u.Code == id);

            return _CompromisosRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<Compromisos> FindBySpec(bool isActive)
         {
             Specification<Compromisos> specification = new DirectSpecification<Compromisos>(u => u.IdCompromiso != null);
            return _CompromisosRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<Compromisos> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<Compromisos> onlyEnabledSpec = new DirectSpecification<Compromisos>(u => u.IdCompromiso != null);

            return _CompromisosRepository.GetPagedElements(pageIndex, pageCount, u => u.IdCompromiso, onlyEnabledSpec, true).ToList();
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

            if (_CompromisosRepository != null)
            {
                _CompromisosRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public List<Compromisos> GetByContrato(int idContrato)
        {
            Specification<Compromisos> specification = new DirectSpecification<Compromisos>(u => u.Fases.IdContrato == idContrato);
            return _CompromisosRepository.GetBySpec(specification).ToList();
        }

        public List<Compromisos> GetByContratoFase(int idContrato, int idFase)
        {
            Specification<Compromisos> specification = new DirectSpecification<Compromisos>(u => u.Fases.IdContrato == idContrato);

            if (idFase != 0)
            {
                specification &= new DirectSpecification<Compromisos>(u => u.IdFase == idFase);
            }

            return _CompromisosRepository.GetCompleteEntityList(specification);
        }


        public Compromisos GetCompleteById(long idCompromiso)
        {
            Specification<Compromisos> specification = new DirectSpecification<Compromisos>(u => u.IdCompromiso == idCompromiso);

            return _CompromisosRepository.GetCompleteEntity(specification);
        }


        public Compromisos GetById(long idCompromiso)
        {
            Specification<Compromisos> specification = new DirectSpecification<Compromisos>(u => u.IdCompromiso == idCompromiso);

            return _CompromisosRepository.GetWitFase(specification);
        }
    }
}
    