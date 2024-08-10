using TasteTrailData.Core.Menus.Models;

namespace TasteTrailExperience.Core.Menus.Services;

public interface IMenuService
{
    Task<IEnumerable<Menu>> GetMenusByCountAsync(int count);

    Task<Menu> GetMenuByIdAsync(int id);

    Task<int> CreateMenuAsync(Menu entity);

    Task<int> DeleteMenuByIdAsync(int id);
    
    Task<int> PutMenuAsync(Menu entity);
}
