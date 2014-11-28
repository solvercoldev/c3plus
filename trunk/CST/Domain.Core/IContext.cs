using Domain.Core.Entities;

namespace Domain.Core
{
    public interface IContext : IUnitOfWork
    {
        /// <summary>
        /// Apply changes made in item or related items in your graph
        /// </summary>
        /// <typeparam name="TEntity">Type of item</typeparam>
        /// <param name="item">Item with changes</param>
        void SetChanges<TEntity>(TEntity item)where TEntity : class, IObjectWithChangeTracker, new();
    }
}