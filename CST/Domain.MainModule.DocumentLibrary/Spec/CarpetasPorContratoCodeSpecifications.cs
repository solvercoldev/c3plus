using System;
using System.Linq;
using System.Linq.Expressions;
using Domain.Core.Specification;
using Domain.MainModules.Entities;

namespace Domain.MainModule.DocumentLibrary.Spec
{
    public class CarpetasPorContratoCodeSpecifications : Specification<TBL_ModuloDocumentosAnexos_Carpetas>
    {
        private readonly string _idContrato = default(string);
        
        public CarpetasPorContratoCodeSpecifications(string idContrato)
        {
            _idContrato = idContrato;
        }

        public override Expression<Func<TBL_ModuloDocumentosAnexos_Carpetas, bool>> SatisfiedBy()
        {
            Specification<TBL_ModuloDocumentosAnexos_Carpetas> spec = new TrueSpecification<TBL_ModuloDocumentosAnexos_Carpetas>();

            spec &= new DirectSpecification<TBL_ModuloDocumentosAnexos_Carpetas>(u => u.IsActive);


            if (!String.IsNullOrEmpty(_idContrato) && !String.IsNullOrWhiteSpace(_idContrato))
            {
                var id = Convert.ToDecimal(_idContrato);
                spec &= new DirectSpecification<TBL_ModuloDocumentosAnexos_Carpetas>(u => u.IdContrato == id);
            }

            return spec.SatisfiedBy();
        }
    }
}