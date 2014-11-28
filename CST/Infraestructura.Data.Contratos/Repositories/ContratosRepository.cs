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
    public class ContratosRepository : GenericRepository<Domain.MainModules.Entities.Contratos>, IContratosRepository 
    {
        IMainModuleUnitOfWork _currentUnitOfWork;

        public ContratosRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
            _currentUnitOfWork = unitOfWork;
        }

        public Domain.MainModules.Entities.Contratos GetCompleteEntity(ISpecification<Domain.MainModules.Entities.Contratos> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.Contratos
                                    .Include(x => x.Empresas)
                                    .Include(x => x.TiposContrato)
                                    .Include(x => x.Bloques)
                                    .Include(x => x.Fases) 
                                    .Include(x => x.TBL_Admin_Usuarios) // Responsable
                                    .Where(specific)
                                    .SingleOrDefault();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }

        public List<Domain.MainModules.Entities.Contratos> GetCompleteEntityList(ISpecification<Domain.MainModules.Entities.Contratos> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.Contratos
                                    .Include(x => x.Empresas)
                                    .Include(x => x.TiposContrato)
                                    .Include(x => x.Bloques)
                                    .Include(x => x.Fases) 
                                    .Include(x => x.TBL_Admin_Usuarios) // Responsable
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
    