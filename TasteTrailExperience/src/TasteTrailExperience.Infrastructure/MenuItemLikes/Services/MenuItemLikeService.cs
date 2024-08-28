using TasteTrailData.Core.MenuItemLikes.Models;
using TasteTrailData.Core.MenuItems.Models;
using TasteTrailData.Core.Users.Models;
using TasteTrailExperience.Core.Common.Exceptions;
using TasteTrailExperience.Core.MenuItemLikes.Dtos;
using TasteTrailExperience.Core.MenuItemLikes.Repositories;
using TasteTrailExperience.Core.MenuItemLikes.Services;
using TasteTrailExperience.Core.MenuItems.Repositories;

namespace TasteTrailExperience.Infrastructure.MenuItemLikes.Services;

public class MenuItemLikeService : IMenuItemLikeService
{
    private readonly IMenuItemLikeRepository _menuItemLikeRepository;
    private readonly IMenuItemRepository _menuItemRepository;

    public MenuItemLikeService(IMenuItemLikeRepository menuItemLikeRepository, IMenuItemRepository menuItemRepository)
    {
        _menuItemLikeRepository = menuItemLikeRepository;
        _menuItemRepository = menuItemRepository;
    }

    public async Task<int> CreateMenuItemLikeAsync(MenuItemLikeCreateDto menuItemLikeCreateDto, User user)
    {
        var menuItemLikeToCreate = new MenuItemLike()
        {
            MenuItemId = menuItemLikeCreateDto.MenuItemId,
            UserId = user.Id,
        };

        var id = await _menuItemLikeRepository.CreateAsync(menuItemLikeToCreate);

        var menuItemId = await _menuItemRepository.IncrementLikesAsync(new MenuItem()
        {
            Name = "",
            MenuId = 0,
            Id = menuItemLikeToCreate.MenuItemId,
            UserId = user.Id,
        });

        if(menuItemId is null)
        {
            await _menuItemLikeRepository.DeleteByIdAsync(id);
        }

        return id;
    }

    public async Task<int?> DeleteMenuItemLikeByIdAsync(int id, User user)
    {
        if (id <= 0)
            throw new ArgumentException($"Invalid ID value: {id}.");

        var menuItemLikeToDelete = await _menuItemLikeRepository.GetAsNoTrackingAsync(id);

        if (menuItemLikeToDelete is null)
            return null;

        if (menuItemLikeToDelete.UserId != user.Id)
            throw new ForbiddenAccessException();

        var menuItemLikeId = await _menuItemLikeRepository.DeleteByIdAsync(id);
        
        var menuItemId = await _menuItemRepository.DecrementLikesAsync(new MenuItem()
        {
            Name = "",
            MenuId = 0,
            Id = menuItemLikeToDelete.MenuItemId,
            UserId = user.Id,
        });

        if(menuItemId is null)
        {
            await _menuItemLikeRepository.CreateAsync(new MenuItemLike()
            {
                MenuItemId = menuItemLikeToDelete.MenuItemId,
                UserId = user.Id,
            });
        }

        return menuItemLikeId;
    }
}
