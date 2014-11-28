using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfFormatosIndiceManagementServices : ISfFormatosIndiceManagementServices
    {

         #region Fields
         readonly IFormatosIndiceRepository _FormatosIndiceRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfFormatosIndiceManagementServices( IFormatosIndiceRepository FormatosIndiceRepository)
         {
            if (FormatosIndiceRepository == null)
                throw new ArgumentNullException("FormatosIndiceRepository");
            _FormatosIndiceRepository = FormatosIndiceRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public FormatosIndice NewEntity()
         {
            return new FormatosIndice();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(FormatosIndice entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _FormatosIndiceRepository.UnitOfWork;
            _FormatosIndiceRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(FormatosIndice entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _FormatosIndiceRepository.UnitOfWork;
            _FormatosIndiceRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(FormatosIndice entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _FormatosIndiceRepository.UnitOfWork;

            _FormatosIndiceRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public FormatosIndice FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<FormatosIndice> specification = new DirectSpecification<FormatosIndice>(u => u.NombreIndice == id.ToString());

            return _FormatosIndiceRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public FormatosIndice FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<FormatosIndice> specification = new DirectSpecification<FormatosIndice>(u => u.Code == id);

            return _FormatosIndiceRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<FormatosIndice> FindBySpec(bool isActive)
         {
             Specification<FormatosIndice> specification = new DirectSpecification<FormatosIndice>(u => u.NombreIndice != null);
            return _FormatosIndiceRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<FormatosIndice> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<FormatosIndice> onlyEnabledSpec = new DirectSpecification<FormatosIndice>(u => u.NombreIndice != null);

            return _FormatosIndiceRepository.GetPagedElements(pageIndex, pageCount, u => u.NombreIndice, onlyEnabledSpec, true).ToList();
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

            if (_FormatosIndiceRepository != null)
            {
                _FormatosIndiceRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    