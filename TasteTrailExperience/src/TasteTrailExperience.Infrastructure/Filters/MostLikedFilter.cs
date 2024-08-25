using TasteTrailExperience.Core.Filters;
using TasteTrailExperience.Core.Filters.Base;

namespace TasteTrailExperience.Infrastructure.Filters;

public class MostLikedFilter : IFilterSpecification<ILikeable>
{
    public IQueryable<ILikeable> Apply(IQueryable<ILikeable> query)
    {
        return query.OrderByDescending(e => e.Likes);
    }
}
