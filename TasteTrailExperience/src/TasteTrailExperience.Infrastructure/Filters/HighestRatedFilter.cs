using TasteTrailExperience.Core.Filters;
using TasteTrailExperience.Core.Filters.Base;

namespace TasteTrailExperience.Infrastructure.Filters;

public class HighestRatedFilter : IFilterSpecification<IRateable>
{
    public IQueryable<IRateable> Apply(IQueryable<IRateable> query)
    {
        return query.OrderByDescending(e => e.Rating);
    }
}
