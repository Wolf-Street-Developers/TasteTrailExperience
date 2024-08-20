using TasteTrailData.Core.MenuItems.Models;
using TasteTrailData.Core.Users.Models;
using TasteTrailExperience.Core.Common.Exceptions;
using TasteTrailExperience.Core.MenuItems.Dtos;
using TasteTrailExperience.Core.MenuItems.Repositories;
using TasteTrailExperience.Core.MenuItems.Services;
using TasteTrailExperience.Core.Menus.Repositories;

namespace TasteTrailExperience.Infrastructure.MenuItems.Services;

public class MenuItemService : IMenuItemService
{
    private readonly IMenuItemRepository _menuItemRepository;

    private readonly IMenuRepository _menuRepository;

    public MenuItemService(IMenuItemRepository menuItemRepository, IMenuRepository menuRepository)
    {
        _menuItemRepository = menuItemRepository;
        _menuRepository = menuRepository;
    }

    public async Task<List<MenuItem>> GetMenuItemsFromToAsync(int from, int to, int menuId)
    {
        if (from <= 0 || to <= 0 || from > to)
            throw new ArgumentException("Invalid 'from' and/or 'to' values.");
        
        if (menuId <= 0)
            throw new ArgumentException($"Invalid Menu ID: {menuId}.");

        var menuItems = await _menuItemRepository.GetFromToFilterAsync(from, to, mi => mi.MenuId == menuId);

        return menuItems;
    }

    public async Task<MenuItem?> GetMenuItemByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException($"Invalid ID value: {id}.");

        var menuItem = await _menuItemRepository.GetByIdAsync(id);

        return menuItem;
    }

    public async Task<int> CreateMenuItemAsync(MenuItemCreateDto menuItem, User user)
    {
        var menu = await _menuRepository.GetByIdAsync(menuItem.MenuId) ?? 
            throw new ArgumentException($"Menu by ID: {menuItem.MenuId} not found.");

        var newMenuItem = new MenuItem() {
            Name = menuItem.Name,
            Description = menuItem.Description,
            Price = menuItem.Price,
            PopularityRate = menuItem.PopularityRate,
            MenuId = menu.Id,
            UserId = user.Id
        };

        var menuItemId = await _menuItemRepository.CreateAsync(newMenuItem);

        return menuItemId;
    }

    public async Task<int?> DeleteMenuItemByIdAsync(int id, User user)
    {
        if (id <= 0)
            throw new ArgumentException($"Invalid ID value: {id}.");

        var menuItem = await _menuItemRepository.GetAsNoTrackingAsync(id);

        if (menuItem is null)
            return null;

        if (menuItem.UserId != user.Id) 
            throw new ForbiddenAccessException();

        var menuItemId = await _menuItemRepository.DeleteByIdAsync(id);

        return menuItemId;
    }

    public async Task<int?> PutMenuItemAsync(MenuItemUpdateDto menuItem, User user)
    {
        var menuItemToUpdate = await _menuItemRepository.GetAsNoTrackingAsync(menuItem.Id);

        if (menuItemToUpdate is null)
            return null;

        if (menuItemToUpdate.UserId != user.Id)
            throw new ForbiddenAccessException();

        var updatedMenuItem = new MenuItem() {
            Id = menuItem.Id,
            Name = menuItem.Name,
            Description = menuItem.Description,
            Price = menuItem.Price,
            PopularityRate = menuItem.PopularityRate,
            UserId = user.Id
        };

        var menuItemId = await _menuItemRepository.PutAsync(updatedMenuItem);

        return menuItemId;
    }
}
