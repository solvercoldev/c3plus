using System;
using System.Globalization;
using System.Linq;
using Domain.MainModule.Contratos.Contracts;
using Infraestructura.Data.Contratos.Resources;
using Infraestructure.Data.Core;
using Infraestructure.Data.Core.Extensions;
using Infrastructure.CrossCutting.Logging;
using Domain.Core;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Infrastructure.Data.MainModule.UnitOfWork;

namespace Infrastructure.Data.MainModule.Contratos.Repositories
{
    public class EmpresasRepository : GenericRepository<Empresas>, IEmpresasRepository 
    {
        public EmpresasRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
        }

        public Empresas GetCompleteEntity(ISpecification<Empresas> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.Empresas
                                    .Include(x => x.TBL_Admin_Usuarios)
                                    .Include(x => x.TBL_Admin_Usuarios1)
                                    .Where(specific)
                                    .SingleOrDefault();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }
    }
}
    