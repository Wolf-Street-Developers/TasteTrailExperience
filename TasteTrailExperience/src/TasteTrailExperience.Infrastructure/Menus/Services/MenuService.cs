using TasteTrailData.Core.Menus.Models;
using TasteTrailExperience.Core.Menus.Dtos;
using TasteTrailExperience.Core.Menus.Repositories;
using TasteTrailExperience.Core.Menus.Services;

namespace TasteTrailExperience.Infrastructure.Menus.Services;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _menuRepository;

    public MenuService(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<List<MenuGetCountDto>> GetMenusByCountAsync(int count)
    {
        var menus = await _menuRepository.GetByCountAsync(count);

        var menuDtos = menus.Select(menu => new MenuGetCountDto
        {
            Id = menu.Id,
            Name = menu.Name,
            Description = menu.Description,
            VenueId = menu.VenueId,
        }).ToList();

        return menuDtos;
    }

    public async Task<MenuGetByIdDto?> GetMenuByIdAsync(int id)
    {
        var menu = await _menuRepository.GetByIdAsync(id);

        if (menu is null)
            return null;

        var menuDto = new MenuGetByIdDto() 
        {
            Id = menu.Id,
            Name = menu.Name,
            Description = menu.Description,
            VenueId = menu.VenueId,
            MenuItems = menu.MenuItems
        };

        return menuDto;
    }

    public async Task<int> CreateMenuAsync(MenuCreateDto menu)
    {
        var newMenu = new Menu() {
            Name = menu.Name,
            Description = menu.Description,
            VenueId = menu.VenueId,
        };

        var menuId = await _menuRepository.CreateAsync(newMenu);

        return menuId;
    }

    public async Task<int?> DeleteMenuByIdAsync(int id)
    {
        var menuId = await _menuRepository.DeleteByIdAsync(id);

        return menuId;
    }

    public async Task<int?> PutMenuAsync(MenuUpdateDto menu)
    {
        var updatedMenu = new Menu() {
            Id = menu.Id,
            Name = menu.Name,
            Description = menu.Description,
            VenueId = menu.VenueId
        };

        var menuId = await _menuRepository.PutAsync(updatedMenu);

        return menuId;
    }
}
