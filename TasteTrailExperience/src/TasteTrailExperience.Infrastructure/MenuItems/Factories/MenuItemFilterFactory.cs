using TasteTrailData.Core.Filters.Enums;
using TasteTrailData.Core.Filters.Specifications;
using TasteTrailData.Core.MenuItems.Models;
using TasteTrailData.Infrastructure.Filters;

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
