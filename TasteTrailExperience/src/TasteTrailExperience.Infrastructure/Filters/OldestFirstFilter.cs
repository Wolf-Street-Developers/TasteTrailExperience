using TasteTrailExperience.Core.Filters;
using TasteTrailExperience.Core.Filters.Base;

namespace TasteTrailExperience.Infrastructure.Filters;

public class OldestFirstFilter : IFilterSpecification<ICreateable>
{
    public IQueryable<ICreateable> Apply(IQueryable<ICreateable> query)
    {
        return query.OrderBy(e => e.CreationDate);
    }
}
