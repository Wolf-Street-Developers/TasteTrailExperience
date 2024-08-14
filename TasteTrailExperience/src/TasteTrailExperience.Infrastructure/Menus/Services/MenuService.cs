using TasteTrailData.Core.Menus.Models;
using TasteTrailExperience.Core.Menus.Dtos;
using TasteTrailExperience.Core.Menus.Repositories;
using TasteTrailExperience.Core.Menus.Services;
using TasteTrailExperience.Core.Venues.Repositories;

namespace TasteTrailExperience.Infrastructure.Menus.Services;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _menuRepository;

    public MenuService(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<List<MenuGetByCountDto>> GetMenusByCountAsync(int count)
    {
        var menus = await _menuRepository.GetByCountAsync(count);

        var menuDtos = menus.Select(menu => new MenuGetByCountDto
        {
            Id = menu.Id,
            Name = menu.Name,
            Description = menu.Description,
            VenueId = menu.VenueId,
        }).ToList();

        return menuDtos;
    }

    public async Task<Menu?> GetMenuByIdAsync(int id)
    {
        var menu = await _menuRepository.GetByIdAsync(id);

        return menu;
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
