using ECommerce.Domain.Contracts;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.Services.Implementation.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecifications(Expression<Func<TEntity, bool>> CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }

        #region Filteration
        public Expression<Func<TEntity, bool>> Criteria { get; }
        #endregion

        #region Pagination
        public int Skip { private set; get; }

        public int Take { private set; get; }

        public bool IsPaginated { private set; get; }

        protected void ApplyPagination(int pageSize, int pageIndex)
        {
            IsPaginated = true;
            Skip = pageSize * (pageIndex - 1);
            Take = pageSize;
        }
        #endregion

        #region Including
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];  // we use get to use value at accessor in hidden field 
        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }
        #endregion

        #region Ordering 
        public Expression<Func<TEntity, object>> OrderBy { private set; get; }

        public Expression<Func<TEntity, object>> OrderByDescending { private set; get; }
        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }
        #endregion
    }
}
