using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Store.Omda.Core.Entities;
using Store.Omda.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Omda.Repository
{
    public class SpecificationsEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery , ISpecifications<TEntity ,TKey> spec)
        {
            var query = inputQuery;

            if (spec.criteria is not null)
            {
                query = query.Where(spec.criteria);
            }

            if (spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending is not null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }
            if (spec.IsPagination)
            {
                query = query.Skip((int)spec.Skip).Take((int)spec.Take);
            }
            query = spec.Includes.Aggregate(query,(currentQuery , IncludeExpression) => currentQuery.Include(IncludeExpression));

            return query;
        }
    }
}
