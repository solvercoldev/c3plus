using System;
using System.Linq.Expressions;

namespace Domain.Core.Specification
{
    public interface ISpecification<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Check if this specification is satisfied by a 
        /// specific expression lambda
        /// </summary>
        /// <returns></returns>
        Expression<Func<TEntity, bool>> SatisfiedBy();
    }
}