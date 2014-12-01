using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Application.MainModule.Contratos.IServices;

namespace Application.MainModule.Contratos.Services
{
    public class SfBloquesManagementServices : ISfBloquesManagementServices
    {

         #region Fields
         readonly IBloquesRepository _BloquesRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfBloquesManagementServices( IBloquesRepository BloquesRepository)
         {
            if (BloquesRepository == null)
                throw new ArgumentNullException("BloquesRepository");
            _BloquesRepository = BloquesRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public Bloques NewEntity()
         {
            return new Bloques();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(Bloques entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _BloquesRepository.UnitOfWork;
            _BloquesRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(Bloques entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _BloquesRepository.UnitOfWork;
            _BloquesRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(Bloques entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _BloquesRepository.UnitOfWork;

            _BloquesRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }



        public Bloques FindById(int id)
        {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<Bloques> specification = new DirectSpecification<Bloques>(u => u.IdBloque == id.ToString());

            return _BloquesRepository.GetEntityBySpec(specification);
        }
         

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<Bloques> FindBySpec(bool isActive)
         {
             Specification<Bloques> specification = new DirectSpecification<Bloques>(u => u.IdBloque != null);
            return _BloquesRepository.GetBySpec(specification).ToList();
         }

        public object FindByIdString(string idBloque)
        {
            if (string.IsNullOrEmpty(idBloque))
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<Bloques> specification = new DirectSpecification<Bloques>(u => u.IdBloque == idBloque);

            return _BloquesRepository.GetEntityBySpec(specification);
        }

        /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<Bloques> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<Bloques> onlyEnabledSpec = new DirectSpecification<Bloques>(u => u.IdBloque != null);

            return _BloquesRepository.GetPagedElements(pageIndex, pageCount, u => u.Descripcion, onlyEnabledSpec, true).ToList();
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

            if (_BloquesRepository != null)
            {
                _BloquesRepository.UnitOfWork.Dispose();
            }
        }

        #endregion


        public Bloques GetById(string id)
        {
            Specification<Bloques> specification = new DirectSpecification<Bloques>(u => u.IdBloque == id);

            return _BloquesRepository.GetCompleteEntity(specification);
        }
    }
}
    