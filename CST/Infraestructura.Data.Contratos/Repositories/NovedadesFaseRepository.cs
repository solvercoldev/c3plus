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
    public class NovedadesFaseRepository : GenericRepository<NovedadesFase>, INovedadesFaseRepository 
    {
        IMainModuleUnitOfWork _currentUnitOfWork;

        public NovedadesFaseRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
            _currentUnitOfWork = unitOfWork;
        }


        public NovedadesFase GetCompleteEntity(ISpecification<NovedadesFase> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.NovedadesFase
                                    .Include(x => x.Fases)
                                    .Include(x => x.TBL_Admin_Usuarios) // Create By
                                    .Include(x => x.TBL_Admin_Usuarios1) // Modify By
                                    .Where(specific)
                                    .SingleOrDefault();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }

        public List<NovedadesFase> GetCompleteEntityList(ISpecification<NovedadesFase> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.NovedadesFase
                                    .Include(x => x.Fases)
                                    .Include(x => x.TBL_Admin_Usuarios) // Create By
                                    .Include(x => x.TBL_Admin_Usuarios1) // Modify By
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