using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Specifications;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
    {
        var query = inputQuery;

        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
        
        query = spec.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        if (spec.Criteria is not null)
        {
            query = query.Where(spec.Criteria);
        }

        if (spec.OrderBy is not null)
        {
            query = query.OrderBy(spec.OrderBy);
        }

        if (spec.OrderByDescending is not null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        return query;
    }
}
