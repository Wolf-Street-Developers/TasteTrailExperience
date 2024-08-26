using TasteTrailExperience.Core.Filters.Base;
using TasteTrailExperience.Core.Specifications.Filters;

namespace TasteTrailExperience.Infrastructure.Filters;

public class NewestFirstFilter<T> : IFilterSpecification<T> where T : ICreateable
{
    public IQueryable<T> Apply(IQueryable<T> query)
    {
        return query.OrderByDescending(e => e.CreationDate);
    }
}
