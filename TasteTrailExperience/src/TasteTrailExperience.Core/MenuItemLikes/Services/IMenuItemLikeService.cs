using TasteTrailData.Core.MenuItemLikes.Models;
using TasteTrailData.Core.Users.Models;
using TasteTrailExperience.Core.MenuItemLikes.Dtos;


namespace TasteTrailExperience.Core.MenuItemLikes.Services;

public interface IMenuItemLikeService
{
    Task<int> CreateMenuItemLikeAsync(MenuItemLikeCreateDto menuItem, User user);

    Task<int?> DeleteMenuItemLikeByIdAsync(int id, User user);
}
