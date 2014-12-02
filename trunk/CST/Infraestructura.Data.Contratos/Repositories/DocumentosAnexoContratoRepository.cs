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
    public class DocumentosAnexoContratoRepository : GenericRepository<DocumentosAnexoContrato>, IDocumentosAnexoContratoRepository 
    {
        IMainModuleUnitOfWork _currentUnitOfWork;

        public DocumentosAnexoContratoRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager)
            : base(unitOfWork, traceManager)
        {
            _currentUnitOfWork = unitOfWork;
        }

        public DocumentosAnexoContrato GetCompleteEntity(ISpecification<DocumentosAnexoContrato> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.DocumentosAnexoContrato
                                    .Include(x => x.TBL_Admin_Usuarios) // CreateBy
                                    .Include(x => x.TBL_Admin_Usuarios1) // MOdifie                                    
                                    .Where(specific)
                                    .SingleOrDefault();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }

        public List<DocumentosAnexoContrato> GetCompleteEntityList(ISpecification<DocumentosAnexoContrato> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.DocumentosAnexoContrato
                                    .Include(x => x.TBL_Admin_Usuarios) // CreateBy
                                    .Include(x => x.TBL_Admin_Usuarios1) // MOdifie    
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
    