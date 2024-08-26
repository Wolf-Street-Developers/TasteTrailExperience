using TasteTrailData.Core.Feedbacks.Models;
using TasteTrailExperience.Core.Filters.Enums;
using TasteTrailExperience.Core.Specifications.Filters;
using TasteTrailExperience.Infrastructure.Filters;

namespace TasteTrailExperience.Infrastructure.Feedbacks.Factories;

public class FeedbackFilterFactory
{
    public static IFilterSpecification<Feedback>? CreateFilter(FilterType? filterType)
    {
        if (filterType is null)
            return null;

        return filterType switch
        {
            FilterType.MostLiked => new MostLikedFilter<Feedback>(),
            FilterType.NewestFirst => new NewestFirstFilter<Feedback>(),
            FilterType.OldestFirst => new OldestFirstFilter<Feedback>(),
            FilterType.HighestRated => new HighestRatedFilter<Feedback>(),
            FilterType.LowestRated => new LowestRatedFilter<Feedback>(),
            _ => throw new ArgumentException("Invalid filter type", nameof(filterType))
        };
    }
}