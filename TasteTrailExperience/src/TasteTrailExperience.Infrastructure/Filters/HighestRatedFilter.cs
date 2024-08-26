using TasteTrailExperience.Core.Filters.Base;
using TasteTrailExperience.Core.Specifications.Filters;

namespace TasteTrailExperience.Infrastructure.Filters;

public class HighestRatedFilter<T> : IFilterSpecification<T> where T : IRateable
{
    public IQueryable<T> Apply(IQueryable<T> query)
    {
        return query.OrderByDescending(e => e.Rating);
    }
}
