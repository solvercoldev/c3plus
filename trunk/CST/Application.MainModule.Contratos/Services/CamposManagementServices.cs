using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfCamposManagementServices : ISfCamposManagementServices
    {

         #region Fields
         readonly ICamposRepository _CamposRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfCamposManagementServices( ICamposRepository CamposRepository)
         {
            if (CamposRepository == null)
                throw new ArgumentNullException("CamposRepository");
            _CamposRepository = CamposRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public Campos NewEntity()
         {
            return new Campos();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(Campos entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _CamposRepository.UnitOfWork;
            _CamposRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(Campos entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _CamposRepository.UnitOfWork;
            _CamposRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(Campos entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _CamposRepository.UnitOfWork;

            _CamposRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public Campos FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<Campos> specification = new DirectSpecification<Campos>(u => u.IdCampo == id.ToString());

            return _CamposRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public Campos FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<Campos> specification = new DirectSpecification<Campos>(u => u.Code == id);

            return _CamposRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<Campos> FindBySpec(bool isActive)
         {
             Specification<Campos> specification = new DirectSpecification<Campos>(u => u.IdCampo != null);
            return _CamposRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<Campos> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<Campos> onlyEnabledSpec = new DirectSpecification<Campos>(u => u.IdCampo != null);

            return _CamposRepository.GetPagedElements(pageIndex, pageCount, u => u.Descripcion, onlyEnabledSpec, true).ToList();
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

            if (_CamposRepository != null)
            {
                _CamposRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public List<Campos> GetByBloque(string idBloque)
        {
            Specification<Campos> specification = new DirectSpecification<Campos>(u => u.IdBloque == idBloque);
            return _CamposRepository.GetBySpec(specification).ToList();
        }
    }
}
    