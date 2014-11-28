using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Domain.Core;
using Domain.Core.Entities;
using Domain.Core.Specification;
using Infraestructure.Data.Core.Extensions;
using Infrastructure.CrossCutting;
using Infrastructure.CrossCutting.Logging;

namespace Infraestructure.Data.Core
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
         where TEntity : class,IObjectWithChangeTracker, new()
    {
        #region Members

        readonly ITraceManager _traceManager;
        readonly IQueryableUnitOfWork _currentUoW;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of Repository
        /// </summary>
        /// <param name="traceManager">Trace Manager dependency</param>
        /// <param name="unitOfWork">A unit of work for this repository</param>
        public GenericRepository(IQueryableUnitOfWork unitOfWork,ITraceManager traceManager)
        {
            //check preconditions
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork", Resources.Messages.exception_ContainerCannotBeNull);

            if (traceManager == null)
                throw new ArgumentNullException("traceManager", Resources.Messages.exception_TraceManagerCannotBeNull);

            //set internal values
            _currentUoW = unitOfWork;
            _traceManager = traceManager;


            //_traceManager.LogInfo(string.Format(CultureInfo.InvariantCulture,
            //                      Resources.Messages.trace_ConstructRepository, typeof(TEntity).Name),
            //                      LogType.Notify);

        }

        #endregion

        #region IRepository<TEntity> Members

        /// <summary>
        /// Return a unit of work in this repository
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _currentUoW;
            }
        }

        public TEntity NewEntity()
        {
            return new TEntity();
        }

        /// <summary>
       /// 
       /// </summary>
       /// <param name="item"></param>
        public virtual void Add(TEntity item)
        {
            //check item
            if (item == null)
                throw new ArgumentNullException("item", Resources.Messages.exception_ItemArgumentIsNull);

            //add object to IObjectSet for this type

            //really for STE you have two options, addobject and
            //call to ApplyChanges in this objetSet. After 
            //review discussion feedback in our codeplex project
            //ApplyChanges is the best option because solve problems in 
            //many to many associations and AddObject method

            if (item.ChangeTracker != null
                &&
                item.ChangeTracker.State == ObjectState.Added)
            {

               
                    //_traceManager.LogInfo("Hasta aqui todo OK",LogType.Notify);

                    _currentUoW.RegisterChanges(item);

                    //_traceManager.LogInfo(string.Format(CultureInfo.InvariantCulture,
                    //                                    Resources.Messages.trace_AddedItemRepository,
                    //                                    typeof (TEntity).Name),
                    //                      LogType.Notify);

            }
            else
                throw new InvalidOperationException(Resources.Messages.exception_ChangeTrackerIsNullOrStateIsNOK);
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="item"></param>
        public virtual void Remove(TEntity item)
        {
            //check item
            if (item == null)
                throw new ArgumentNullException("item", Resources.Messages.exception_ItemArgumentIsNull);


            var objectSet = CreateSet();

            //Attach object to unit of work and delete this
            // this is valid only if T is a type in model
            objectSet.Attach(item);
            
            //delete object to IObjectSet for this type
            objectSet.DeleteObject(item);
            
            //_traceManager.LogInfo(string.Format(CultureInfo.InvariantCulture,
            //                      Resources.Messages.trace_DeletedItemRepository,
            //                      typeof(TEntity).Name),
            //                      LogType.Notify);

          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public virtual void RegisterItem(TEntity item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            (CreateSet()).Attach(item);

            //_traceManager.LogInfo(string.Format(CultureInfo.InvariantCulture,
            //                      Resources.Messages.trace_AttachedItemToRepository,
            //                      typeof(TEntity).Name),
            //                      LogType.Notify);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public virtual void Modify(TEntity item)
        {
            //check arguments
            if (item == null)
                throw new ArgumentNullException("item", Resources.Messages.exception_ItemArgumentIsNull);

            //Set modifed state if change tracker is enabled and state is not deleted
            if (item.ChangeTracker != null
                &&
                ((item.ChangeTracker.State & ObjectState.Deleted) != ObjectState.Deleted)
               )
            {
                item.MarkAsModified();
            }
            //apply changes for item object
            _currentUoW.RegisterChanges(item);

            //_traceManager.LogInfo(string.Format(CultureInfo.InvariantCulture,
            //                      Resources.Messages.trace_AppliedChangedItemRepository,
            //                      typeof(TEntity).Name),
            //                      LogType.Notify);

        }
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            //_traceManager.LogInfo(string.Format(CultureInfo.InvariantCulture,
            //                      Resources.Messages.trace_GetAllRepository,
            //                      typeof(TEntity).Name),
            //                      LogType.Notify);
            
            //Create IObjectSet and perform query 
            return (CreateSet()).AsEnumerable();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetBySpec(ISpecification<TEntity> specification)
        {
            if (specification == null)
                throw new ArgumentNullException("specification");

            //_traceManager.LogInfo(string.Format("{0} - {1} - {2}",CultureInfo.InvariantCulture,
            //                      Resources.Messages.trace_GetBySpec,
            //                      typeof(TEntity).Name),
            //                      LogType.Notify);

            return (CreateSet().Where(specification.SatisfiedBy())
                                     .AsEnumerable());
           
        }

        public int BulkDeletebySpec(ISpecification<TEntity> specification)
        {
            if (specification == null)
                throw new ArgumentNullException("specification");

            //_traceManager.LogInfo(string.Format("{0} - {1} - {2}", CultureInfo.InvariantCulture,
            //                      Resources.Messages.trace_GetBySpec,
            //                      typeof(TEntity).Name),
            //                      LogType.Notify);

            return (CreateSet().BulkDelete(specification.SatisfiedBy()));
        }


        public int BulkUpdatebySpec(ISpecification<TEntity> specification, Expression<Func<TEntity, TEntity>> evaluator)
        {
            if (specification == null)
                throw new ArgumentNullException("specification");

            //_traceManager.LogInfo(string.Format("{0} - {1} - {2}", CultureInfo.InvariantCulture,
            //                      Resources.Messages.trace_GetBySpec,
            //                      typeof(TEntity).Name),
            //                      LogType.Notify);

            return (CreateSet().BulkUpdate(evaluator, specification.SatisfiedBy()));
        }

        public TEntity GetEntityBySpec(ISpecification<TEntity> specification)
        {
            if (specification == null)
                throw new ArgumentNullException("specification");

            //_traceManager.LogInfo(string.Format("{0} - {1} - {2}", CultureInfo.InvariantCulture,
            //                      Resources.Messages.trace_GetBySpec,
            //                      typeof(TEntity).Name),
            //                      LogType.Notify);

            return (CreateSet().Where(specification.SatisfiedBy()).SingleOrDefault());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetPagedElements<TS>(int pageIndex, int pageCount, Expression<Func<TEntity, TS>> orderByExpression, bool ascending)
        {
            //checking arguments for this query 
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");

            if (orderByExpression == null)
                throw new ArgumentNullException("orderByExpression", Resources.Messages.exception_OrderByExpressionCannotBeNull);

            //_traceManager.LogInfo(string.Format(CultureInfo.InvariantCulture,
            //                     Resources.Messages.trace_GetPagedElementsRepository,
            //                     typeof(TEntity).Name, pageIndex, pageCount, orderByExpression),
            //                     LogType.Notify);

            //Create associated IObjectSet and perform query

            var objectSet = CreateSet();

            
            return objectSet.Paginate(orderByExpression, pageIndex, pageCount, ascending);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="specification"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetPagedElements<TS>(int pageIndex, int pageCount, Expression<Func<TEntity, TS>> orderByExpression, ISpecification<TEntity> specification, bool ascending)
        {
            //checking arguments for this query 
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");

            if (orderByExpression == null)
                throw new ArgumentNullException("orderByExpression", Resources.Messages.exception_OrderByExpressionCannotBeNull);

            if (specification == null)
                throw new ArgumentNullException("specification", Resources.Messages.exception_SpecificationIsNull);

            //_traceManager.LogInfo(string.Format(CultureInfo.InvariantCulture,
            //                      Resources.Messages.trace_GetPagedElementsRepository,
            //                      typeof(TEntity).Name, pageIndex, pageCount, orderByExpression),
            //                      LogType.Notify);

           //Create associated IObjectSet and perform query

            var objectSet = CreateSet();

            //this query cannot  use Paginate IQueryable extension method because Linq queries cannot be
            //merged with Object Builder methods. See Entity Framework documentation for more information

            return (ascending)
                                ?
                                    objectSet
                                     .Where(specification.SatisfiedBy())
                                     .OrderBy(orderByExpression)
                                     .Skip(pageIndex * pageCount)
                                     .Take(pageCount)
                                     .ToList() 
                                :
                                    objectSet
                                     .Where(specification.SatisfiedBy())
                                     .OrderByDescending(orderByExpression)
                                     .Skip(pageIndex * pageCount)
                                     .Take(pageCount)
                                     .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetFilteredElements(Expression<Func<TEntity, bool>> filter)
        {
            //checking query arguments
            if (filter == null)
                throw new ArgumentNullException("filter", Resources.Messages.exception_FilterCannotBeNull);

            //_traceManager.LogInfo(string.Format(CultureInfo.InvariantCulture,
            //                     Resources.Messages.trace_GetFilteredElementsRepository,
            //                     typeof(TEntity).Name, filter),
            //                     LogType.Notify);

           

          //Create IObjectSet and perform query
            return CreateSet().Where(filter)
                                    .ToList(); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <param name="filter"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetFilteredElements<TS>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TS>> orderByExpression, bool ascending)
        {
            //Checking query arguments
            if (filter == null)
                throw new ArgumentNullException("filter", Resources.Messages.exception_FilterCannotBeNull);

            if (orderByExpression == null)
                throw new ArgumentNullException("orderByExpression", Resources.Messages.exception_OrderByExpressionCannotBeNull);

            //_traceManager.LogInfo(string.Format(CultureInfo.InvariantCulture,
            //                     Resources.Messages.trace_GetFilteredElementsRepository,
            //                     typeof(TEntity).Name, filter),
            //                     LogType.Notify);

            //Create IObjectSet for this type and perform query
            var objectSet = CreateSet();

            return (ascending)
                                ?
                                    objectSet
                                     .Where(filter)
                                     .OrderBy(orderByExpression)
                                     .ToList()
                                :
                                    objectSet
                                     .Where(filter)
                                     .OrderByDescending(orderByExpression)
                                     .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetFilteredElements<TS>(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageCount, Expression<Func<TEntity, TS>> orderByExpression, bool ascending)
        {

            //checking query arguments
            if (filter == null)
                throw new ArgumentNullException("filter", Resources.Messages.exception_FilterCannotBeNull);

            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");

            if (orderByExpression == null)
                throw new ArgumentNullException("orderByExpression", Resources.Messages.exception_OrderByExpressionCannotBeNull);

            //_traceManager.LogInfo(string.Format(CultureInfo.InvariantCulture,
            //                      Resources.Messages.trace_GetFilteredPagedElementsRepository,
            //                      typeof(TEntity).Name,filter,pageIndex,pageCount,orderByExpression),
            //                      LogType.Notify);

          
            //Create IObjectSet for this particular type and query this
            var objectSet = CreateSet();

            return (ascending)
                                ?
                                    objectSet
                                     .Where(filter)
                                     .OrderBy(orderByExpression)
                                     .Skip(pageIndex * pageCount)
                                     .Take(pageCount)
                                     .ToList()
                                :
                                    objectSet
                                     .Where(filter)
                                     .OrderByDescending(orderByExpression)
                                     .Skip(pageIndex * pageCount)
                                     .Take(pageCount)
                                     .ToList();


        }

        #endregion

        #region Private Methods

        IObjectSet<TEntity> CreateSet()
        {
            if (_currentUoW != null)
            {
                var objectSet = _currentUoW.CreateSet<TEntity>();
                
                //set merge option to underlying ObjectQuery

                var query = objectSet as ObjectQuery<TEntity>;

                if ( query != null ) // check if this objectset is not in memory object set for testing
                    query.MergeOption = MergeOption.AppendOnly;

                return objectSet;
            }
            throw new InvalidOperationException(Resources.Messages.exception_ContainerCannotBeNull);
        }

        #endregion
    }
}