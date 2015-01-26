using System;
using System.Collections.Generic;
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

namespace Infrastructure.Data.MainModule.Contratos.Repositories
{
    public class RadicadosRepository : GenericRepository<Radicados>, IRadicadosRepository 
    {
        IMainModuleUnitOfWork _currentUnitOfWork;

        public RadicadosRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
            _currentUnitOfWork = unitOfWork;
        }

        public Radicados GetCompleteEntity(ISpecification<Radicados> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.Radicados
                                    .Include(x => x.Contratos)
                                    .Include(x => x.Contratos.Bloques)
                                    .Include(x => x.TBL_Admin_Usuarios)
                                    .Include(x => x.TBL_Admin_Usuarios1)
                                    .Include(x => x.TBL_Admin_Usuarios2)
                                    .Include(x => x.TBL_Admin_Usuarios3)
                                    .Include(x => x.TBL_Admin_Usuarios4)
                                    .Include(x => x.Radicados2)                                    
                                    .Where(specific)
                                    .SingleOrDefault();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }

        public List<Radicados> GetCompleteEntityList(ISpecification<Radicados> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.Radicados
                                    .Include(x => x.TBL_Admin_Usuarios)
                                    .Include(x => x.TBL_Admin_Usuarios1)
                                    .Include(x => x.TBL_Admin_Usuarios2)
                                    .Include(x => x.TBL_Admin_Usuarios3)
                                    .Include(x => x.TBL_Admin_Usuarios4)
                                    .Include(x => x.Radicados2)
                                    .Where(specific)
                                    .ToList();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }
    }
}
    