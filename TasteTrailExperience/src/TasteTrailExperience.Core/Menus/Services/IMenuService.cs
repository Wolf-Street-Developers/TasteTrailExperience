using TasteTrailData.Core.Menus.Models;
using TasteTrailExperience.Core.Menus.Dtos;

namespace TasteTrailExperience.Core.Menus.Services;

public interface IMenuService
{
    Task<List<MenuGetCountDto>> GetMenusByCountAsync(int count);

    Task<MenuGetByIdDto?> GetMenuByIdAsync(int id);

    Task<int> CreateMenuAsync(MenuCreateDto menu);

    Task<int?> DeleteMenuByIdAsync(int id);
    
    Task<int?> PutMenuAsync(MenuUpdateDto menu);
}
