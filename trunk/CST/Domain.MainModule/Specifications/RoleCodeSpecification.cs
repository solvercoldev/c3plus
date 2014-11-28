using System;
using System.Linq.Expressions;
using Domain.Core.Specification;
using Domain.MainModules.Entities;

namespace Domain.MainModule.Specifications
{
    public class RoleCodeSpecification : Specification<TBL_Admin_Roles>
    {

         #region Members

        readonly string _roleName;

        #endregion

         #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="roleName"></param>
        public RoleCodeSpecification(string roleName)
        {
            _roleName = roleName;
        }

        #endregion

        public override Expression<Func<TBL_Admin_Roles, bool>> SatisfiedBy()
        {
            Specification<TBL_Admin_Roles> spec = new TrueSpecification<TBL_Admin_Roles>();

            if (!String.IsNullOrEmpty(_roleName)
                &&
                !String.IsNullOrWhiteSpace(_roleName))
            {
                spec &= new DirectSpecification<TBL_Admin_Roles>(r => r.NombreRol.Contains(_roleName));
            }

            return spec.SatisfiedBy();
        }
    }
}