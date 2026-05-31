using ECommerce.Domain.Contracts;
using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Persistence
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> entiryQuery, ISpecifications<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var query = entiryQuery;
            if (specifications != null)
            {
                if(specifications.Criteria is not null)
                {
                    query = query.Where(specifications.Criteria);
                }
                if (specifications.IncludeExpressions != null && specifications.IncludeExpressions.Any())
                {
                    query = specifications.IncludeExpressions.Aggregate(query, (current, include)
                                                              => current.Include(include));
                }
                if (specifications.OrderBy != null)
                {
                    query = query.OrderBy(specifications.OrderBy);
                }
                if (specifications.OrderByDescending != null)
                {
                    query = query.OrderByDescending(specifications.OrderByDescending);
                }
                if(specifications.IsPaginated)
                {
                    query = query.Skip(specifications.Skip).Take(specifications.Take);
                }
            }

            return query;
        }
    }
}
