using TasteTrailData.Core.MenuItems.Models;
using TasteTrailExperience.Core.MenuItems.Dtos;

namespace TasteTrailExperience.Core.MenuItems.Services;

public interface IMenuItemService
{
    Task<List<MenuItem>> GetMenuItemsByCountAsync(int count);

    Task<MenuItem?> GetMenuItemByIdAsync(int id);

    Task<int> CreateMenuItemAsync(MenuItemCreateDto menuItem);

    Task<int?> DeleteMenuItemByIdAsync(int id);
    
    Task<int?> PutMenuItemAsync(MenuItemUpdateDto menuItem);
}
