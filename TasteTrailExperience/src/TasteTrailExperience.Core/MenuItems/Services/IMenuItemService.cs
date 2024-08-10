using TasteTrailData.Core.MenuItems.Models;

namespace TasteTrailExperience.Core.MenuItems.Services;

public interface IMenuItemService
{
    Task<IEnumerable<MenuItem>> GetByMenuItemsCountAsync(int count);

    Task<MenuItem> GetMenuItemByIdAsync(int id);

    Task<int> CreateMenuItemAsync(MenuItem menuItem);

    Task<int> DeleteMenuItemByIdAsync(int id);
    
    Task<int> PutMenuItemAsync(MenuItem entity);
}
