using TasteTrailData.Core.Menus.Models;
using TasteTrailData.Core.Users.Models;
using TasteTrailExperience.Core.Menus.Dtos;

namespace TasteTrailExperience.Core.Menus.Services;

public interface IMenuService
{
    Task<List<MenuGetByCountDto>> GetMenusFromToAsync(int from, int to);

    Task<Menu?> GetMenuByIdAsync(int id);

    Task<int> CreateMenuAsync(MenuCreateDto menu, User user);

    Task<int?> DeleteMenuByIdAsync(int id, User user);
    
    Task<int?> PutMenuAsync(MenuUpdateDto menu, User user);
}
