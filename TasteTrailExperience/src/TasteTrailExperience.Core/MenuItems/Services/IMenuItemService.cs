using TasteTrailData.Core.MenuItems.Models;
using TasteTrailData.Core.Users.Models;
using TasteTrailExperience.Core.Filters.Dtos;
using TasteTrailExperience.Core.MenuItems.Dtos;

namespace TasteTrailExperience.Core.MenuItems.Services;

public interface IMenuItemService
{
    Task<FilterResponseDto<MenuItem>> GetMenuItemsFiltered(FilterParametersDto filterParameters, int menuId);

    Task<MenuItem?> GetMenuItemByIdAsync(int id);

    Task<int> CreateMenuItemAsync(MenuItemCreateDto menuItem, User user);

    Task<int?> DeleteMenuItemByIdAsync(int id, User user);
    
    Task<int?> PutMenuItemAsync(MenuItemUpdateDto menuItem, User user);
}
