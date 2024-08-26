using TasteTrailExperience.Core.Filters.Base;
using TasteTrailExperience.Core.Specifications.Filters;

namespace TasteTrailExperience.Infrastructure.Filters;

public class LowestRatedFilter<T> : IFilterSpecification<T> where T : IRateable
{
    public IQueryable<T> Apply(IQueryable<T> query)
    {
        return query.OrderBy(e => e.Rating);
    }
}
