using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;

namespace Infraestructure.Data.Core
{
    public sealed class MemorySet<TEntity> : IObjectSet<TEntity>
       where TEntity : class
    {
        #region Members

        readonly List<TEntity> _innerList;
        readonly List<string> _includePaths;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor 
        /// </summary>
        /// <param name="innerList">A List{T} with inner values of this IObjectSet</param>
        public MemorySet(List<TEntity> innerList)
        {
            if (innerList == null)
                throw new ArgumentNullException("innerList");

            _innerList = innerList;
            _includePaths = new List<string>();


        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Include path in query objects, 
        /// only for support this ObjectQuery method in test
        /// </summary>
        /// <param name="path">Path to include</param>
        /// <returns>IObjectSet with include path</returns>
        public MemorySet<TEntity> Include(string path)
        {
            if (String.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");

            _includePaths.Add(path);

            return this;
        }

        /// <summary>
        /// OfType method for mock the similar method
        /// in ObjectQuery. For test purposes only
        /// </summary>
        /// <typeparam name="TKEntity">The subclass type</typeparam>
        /// <returns>A new memory set for <typeparamref name="TKEntity"/></returns>
        public MemorySet<TKEntity> OfType<TKEntity>()
            where TKEntity : class
        {
            var kList = _innerList.OfType<TKEntity>()
                                  .ToList();

            return new MemorySet<TKEntity>(kList);
        }
        #endregion

        #region IObjectSet<T> Members

        /// <summary>
        /// <see cref="System.Data.Objects.IObjectSet{T}"/>
        /// </summary>
        /// <param name="entity"><see cref="System.Data.Objects.IObjectSet{T}"/></param>
        public void AddObject(TEntity entity)
        {
            if (entity != null)
                _innerList.Add(entity);
        }
        /// <summary>
        /// <see cref="System.Data.Objects.IObjectSet{T}"/>
        /// </summary>
        /// <param name="entity"><see cref="System.Data.Objects.IObjectSet{T}"/></param>
        public void Attach(TEntity entity)
        {
            if (entity != null
                &&
                !_innerList.Contains(entity))
            {
                _innerList.Add(entity);
            }
        }
        /// <summary>
        /// <see cref="System.Data.Objects.IObjectSet{T}"/>
        /// </summary>
        /// <param name="entity"><see cref="System.Data.Objects.IObjectSet{T}"/></param>
        public void Detach(TEntity entity)
        {
            if (entity != null)
                _innerList.Remove(entity);
        }
        /// <summary>
        /// <see cref="System.Data.Objects.IObjectSet{T}"/>
        /// </summary>
        /// <param name="entity"><see cref="System.Data.Objects.IObjectSet{T}"/></param>
        public void DeleteObject(TEntity entity)
        {
            if (entity != null)
                _innerList.Remove(entity);
        }

        #endregion

        #region IEnumerable<T> Members

        /// <summary>
        /// <see cref="System.Collections.IEnumerable.GetEnumerator"/>
        /// </summary>
        /// <returns><see cref="System.Collections.IEnumerable.GetEnumerator"/></returns>
        public IEnumerator<TEntity> GetEnumerator()
        {
            foreach (TEntity item in _innerList)
                yield return item;
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// <see cref="System.Collections.IEnumerable.GetEnumerator"/>
        /// </summary>
        /// <returns><see cref="System.Collections.IEnumerable.GetEnumerator"/></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region IQueryable Members

        /// <summary>
        /// <see cref="System.Linq.IQueryable{T}"/>
        /// </summary>
        public Type ElementType
        {
            get { return typeof(TEntity); }
        }
        /// <summary>
        /// <see cref="System.Linq.IQueryable{T}"/>
        /// </summary>
        public System.Linq.Expressions.Expression Expression
        {
            get { return _innerList.AsQueryable().Expression; }
        }
        /// <summary>
        /// <see cref="System.Linq.IQueryable{T}"/>
        /// </summary>
        public IQueryProvider Provider
        {
            get { return _innerList.AsQueryable().Provider; }
        }

        #endregion
    }
}