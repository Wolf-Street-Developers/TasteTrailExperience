using TasteTrailExperience.Core.Filters;
using TasteTrailExperience.Core.Filters.Base;

namespace TasteTrailExperience.Infrastructure.Filters;

public class NewestFirstFilter : IFilterSpecification<ICreateable>
{
    public IQueryable<ICreateable> Apply(IQueryable<ICreateable> query)
    {
        return query.OrderByDescending(e => e.CreationDate);
    }
}
