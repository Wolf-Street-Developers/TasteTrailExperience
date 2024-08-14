using TasteTrailData.Core.MenuItems.Models;
using TasteTrailExperience.Core.MenuItems.Dtos;
using TasteTrailExperience.Core.MenuItems.Repositories;
using TasteTrailExperience.Core.MenuItems.Services;

namespace TasteTrailExperience.Infrastructure.MenuItems.Services;

public class MenuItemService : IMenuItemService
{
    private readonly IMenuItemRepository _menuItemRepository;

    public MenuItemService(IMenuItemRepository menuItemRepository)
    {
        _menuItemRepository = menuItemRepository;
    }

    public async Task<List<MenuItem>> GetMenuItemsByCountAsync(int count)
    {
        var menuItems = await _menuItemRepository.GetByCountAsync(count);

        return menuItems;
    }

    public async Task<MenuItem?> GetMenuItemByIdAsync(int id)
    {
        var menuItem = await _menuItemRepository.GetByIdAsync(id);

        return menuItem;
    }

    public async Task<int> CreateMenuItemAsync(MenuItemCreateDto menuItem)
    {
        var newMenuItem = new MenuItem() {
            Name = menuItem.Name,
            Description = menuItem.Description,
            Price = menuItem.Price,
            PopularityRate = menuItem.PopularityRate,
            MenuId = menuItem.MenuId,
        };

        var menuItemId = await _menuItemRepository.CreateAsync(newMenuItem);

        return menuItemId;
    }

    public async Task<int?> DeleteMenuItemByIdAsync(int id)
    {
        var menuItemId = await _menuItemRepository.DeleteByIdAsync(id);

        return menuItemId;
    }

    public async Task<int?> PutMenuItemAsync(MenuItemUpdateDto menuItem)
    {
        var updatedMenuItem = new MenuItem() {
            Id = menuItem.Id,
            Name = menuItem.Name,
            Description = menuItem.Description,
            Price = menuItem.Price,
            PopularityRate = menuItem.PopularityRate,
            MenuId = menuItem.MenuId,
        };

        var menuItemId = await _menuItemRepository.PutAsync(updatedMenuItem);

        return menuItemId;
    }
}
