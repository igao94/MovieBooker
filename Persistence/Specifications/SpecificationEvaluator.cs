using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Specifications;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
    {
        var query = inputQuery;

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

        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

        query = spec.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        if (spec.IgnoreGlobalQueryFilter) query = query.IgnoreQueryFilters();

        if (spec.IsSplitQuery) query = query.AsSplitQuery();

        return query;
    }

    public static IQueryable<TResult> GetQuery<TResult>(IQueryable<T> inputQuery,
        ISpecification<T, TResult> spec)
    {
        var query = inputQuery;

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

        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

        query = spec.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        if (spec.IgnoreGlobalQueryFilter) query = query.IgnoreQueryFilters();

        if (spec.IsSplitQuery) query = query.AsSplitQuery();

        var selectQuery = query as IQueryable<TResult>;

        if (spec.Select is not null)
        {
            selectQuery = query.Select(spec.Select);
        }

        return selectQuery ?? query.Cast<TResult>();
    }
}
