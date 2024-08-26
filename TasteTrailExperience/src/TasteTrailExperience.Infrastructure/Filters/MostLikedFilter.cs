using TasteTrailExperience.Core.Filters.Base;
using TasteTrailExperience.Core.Specifications.Filters;

namespace TasteTrailExperience.Infrastructure.Filters;

public class MostLikedFilter<T> : IFilterSpecification<T> where T : ILikeable
{
    public IQueryable<T> Apply(IQueryable<T> query)
    {
        return query.OrderByDescending(e => e.Likes);
    }
}
