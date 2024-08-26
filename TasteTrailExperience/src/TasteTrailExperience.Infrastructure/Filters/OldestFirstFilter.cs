using TasteTrailExperience.Core.Filters.Base;
using TasteTrailExperience.Core.Specifications.Filters;

namespace TasteTrailExperience.Infrastructure.Filters;

public class OldestFirstFilter<T> : IFilterSpecification<T> where T : ICreateable
{
    public IQueryable<T> Apply(IQueryable<T> query)
    {
        return query.OrderBy(e => e.CreationDate);
    }
}
