using System;
using System.Globalization;
using System.Linq;
using Domain.Core.Specification;
using Domain.MainModule.Contratos.Contracts;
using Domain.MainModules.Entities;
using Infraestructura.Data.Contratos.Resources;
using Infraestructure.Data.Core;
using Infraestructure.Data.Core.Extensions;
using Infrastructure.CrossCutting.Logging;
using Infrastructure.Data.MainModule.UnitOfWork;
using System.Collections.Generic;

namespace Infrastructure.Data.MainModule.Contratos.Repositories
{
    public class ComentariosRespuestaRepository : GenericRepository<ComentariosRespuesta>, IComentariosRespuestaRepository 
    {
        private IMainModuleUnitOfWork _currentUnitOfWork;

        public ComentariosRespuestaRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
            _currentUnitOfWork = unitOfWork;
        }

        public ComentariosRespuesta GetCompleteEntityBySpec(ISpecification<ComentariosRespuesta> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            if (_currentUnitOfWork != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return _currentUnitOfWork.ComentariosRespuesta
                                        .Include(x => x.TBL_Admin_Usuarios)       // Destino
                                        .Include(x => x.TBL_Admin_Usuarios1)      // Create
                                        .Include(x => x.TBL_Admin_Usuarios2)      // Modify
                                        .Include(x => x.Contratos)                // Reclamo
                                        .Include(x => x.ComentariosRespuesta1)    // Comentarios Relacionados
                                        .Where(specific)
                                        .SingleOrDefault();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }

        public List<ComentariosRespuesta> GetCompleteListBySpec(ISpecification<ComentariosRespuesta> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");
            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return _currentUnitOfWork.ComentariosRespuesta
                                        .Include(x => x.TBL_Admin_Usuarios)       // Destino
                                        .Include(x => x.TBL_Admin_Usuarios1)      // Create
                                        .Include(x => x.TBL_Admin_Usuarios2)      // Modify
                                        .Include(x => x.Contratos)                // Reclamo
                                        .Include(x => x.ComentariosRespuesta1.Select(e => e.TBL_Admin_Usuarios1))    // Comentarios Relacionados
                                        .Where(specific)
                                        .ToList();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }

        public ComentariosRespuesta GetComentarioById(decimal id)
        {
            if (id > 0)
            {
                var set = _currentUnitOfWork.CreateSet<ComentariosRespuesta>();

                return set.Where(c => c.IdComentario == id)
                          .Include(x => x.TBL_Admin_Usuarios)       // Destino
                          .Include(x => x.TBL_Admin_Usuarios1)      // Create
                          .Include(x => x.TBL_Admin_Usuarios2)      // Modify
                          .Include(x => x.Contratos)                // Reclamo
                          .Include(x => x.ComentariosRespuesta1)    // Comentarios Relacionados
                          .Select(c => c)
                          .SingleOrDefault();
            }

            return null;
        }
    }
}
    