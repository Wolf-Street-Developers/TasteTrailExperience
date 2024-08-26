using TasteTrailData.Core.MenuItems.Models;
using TasteTrailExperience.Core.Filters.Enums;
using TasteTrailExperience.Core.Specifications.Filters;
using TasteTrailExperience.Infrastructure.Filters;

namespace TasteTrailExperience.Infrastructure.MenuItems.Factories;

public class MenuItemFilterFactory
{
    public static IFilterSpecification<MenuItem>? CreateFilter(FilterType? filterType)
    {
        if (filterType is null)
            return null;

        return filterType switch
        {
            FilterType.MostLiked => new MostLikedFilter<MenuItem>(),
            _ => throw new ArgumentException("Invalid filter type", nameof(filterType))
        };
    }
}
