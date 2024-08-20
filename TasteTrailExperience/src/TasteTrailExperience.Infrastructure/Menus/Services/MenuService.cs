using TasteTrailData.Core.Menus.Models;
using TasteTrailData.Core.Users.Models;
using TasteTrailExperience.Core.Common.Exceptions;
using TasteTrailExperience.Core.Menus.Dtos;
using TasteTrailExperience.Core.Menus.Repositories;
using TasteTrailExperience.Core.Menus.Services;
using TasteTrailExperience.Core.Venues.Repositories;

namespace TasteTrailExperience.Infrastructure.Menus.Services;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _menuRepository;

    private readonly IVenueRepository _venueRepository;

    public MenuService(IMenuRepository menuRepository, IVenueRepository venueRepository)
    {
        _menuRepository = menuRepository;
        _venueRepository = venueRepository;
    }

    public async Task<List<MenuGetByCountDto>> GetMenusFromToAsync(int from, int to)
    {
        if (from <= 0 || to <= 0 || from > to)
            throw new ArgumentException("Invalid 'from' and/or 'to' values.");

        var menus = await _menuRepository.GetFromToAsync(from, to);

        var menuDtos = menus.Select(menu => new MenuGetByCountDto
        {
            Id = menu.Id,
            Name = menu.Name,
            Description = menu.Description,
            VenueId = menu.VenueId,
            UserId = menu.UserId
        }).ToList();

        return menuDtos;
    }

    public async Task<Menu?> GetMenuByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException($"Invalid ID value: {id}.");

        var menu = await _menuRepository.GetByIdAsync(id);

        return menu;
    }

    public async Task<int> CreateMenuAsync(MenuCreateDto menu, User user)
    {
        var venue = await _venueRepository.GetByIdAsync(menu.VenueId) ?? 
            throw new ArgumentException($"Venue by ID: {menu.VenueId} not found.");

        var newMenu = new Menu() {
            Name = menu.Name,
            Description = menu.Description,
            VenueId = venue.Id,
            UserId = user.Id
        };

        var menuId = await _menuRepository.CreateAsync(newMenu);

        return menuId;
    }

    public async Task<int?> DeleteMenuByIdAsync(int id, User user)
    {
        if (id <= 0)
            throw new ArgumentException($"Invalid ID value: {id}.");

        var menu = await _menuRepository.GetAsNoTrackingAsync(id);

        if (menu is null)
            return null;

        if (menu.UserId != user.Id)
            throw new ForbiddenAccessException();

        var menuId = await _menuRepository.DeleteByIdAsync(id);

        return menuId;
    }

    public async Task<int?> PutMenuAsync(MenuUpdateDto menu, User user)
    {
        var menuToUpdate = await _menuRepository.GetAsNoTrackingAsync(menu.Id);

        if (menuToUpdate is null)
            return null;

        if (menuToUpdate.UserId != user.Id)
            throw new ForbiddenAccessException();

        var updatedMenu = new Menu() 
        {
            Id = menu.Id,
            Name = menu.Name,
            Description = menu.Description
        };

        var menuId = await _menuRepository.PutAsync(updatedMenu);

        return menuId;
    }
}
