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
    public class CompromisosRepository : GenericRepository<Compromisos>, ICompromisosRepository 
    {
        IMainModuleUnitOfWork _currentUnitOfWork;

        public CompromisosRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
            _currentUnitOfWork = unitOfWork;
        }

        public Compromisos GetCompleteEntity(ISpecification<Compromisos> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.Compromisos
                                    .Include(x => x.Fases)
                                    .Include(x => x.Fases.Contratos)
                                    .Include(x => x.Fases.Contratos.Bloques)
                                    .Include(x => x.Pozos)
                                    .Include(x => x.Campos)
                                    .Include(x => x.PagosObligaciones)
                                    .Include(x => x.PagosObligaciones.Select(e => e.Monedas))
                                    .Include(x => x.PagosObligaciones.Select(e => e.Monedas1))
                                    .Include(x => x.PagosObligaciones.Select(e => e.Terceros))
                                    .Include(x => x.PagosObligaciones.Select(e => e.TiposPagoObligacion))
                                    .Include(x => x.EntregablesANHCompromiso)
                                    .Include(x => x.EntregablesANHCompromiso.Select(e => e.ManualAnh))
                                    .Include(x => x.TBL_Admin_Usuarios) // CreateBy
                                    .Include(x => x.TBL_Admin_Usuarios1) // MOdifie                                    
                                    .Include(x => x.TBL_Admin_Usuarios2) // Responsable     
                                    .Where(specific)
                                    .SingleOrDefault();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }

        public Compromisos GetWitFase(ISpecification<Compromisos> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.Compromisos
                                    .Include(x => x.Fases)
                                    .Where(specific)
                                    .SingleOrDefault();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));
        }

        public List<Compromisos> GetCompleteEntityList(ISpecification<Compromisos> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");

            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {

                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.Compromisos
                                    .Include(x => x.Fases)
                                    .Include(x => x.Fases.Contratos)
                                    .Include(x => x.Fases.Contratos.Bloques)
                                    .Include(x => x.Pozos)
                                    .Include(x => x.Campos)
                                    .Include(x => x.TBL_Admin_Usuarios) // CreateBy
                                    .Include(x => x.TBL_Admin_Usuarios1) // MOdifie                                    
                                    .Include(x => x.TBL_Admin_Usuarios2) // Responsable    
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
    