using Microsoft.AspNetCore.Identity;
using TasteTrailData.Core.Filters.Specifications;
using TasteTrailData.Core.Menus.Models;
using TasteTrailData.Core.Roles.Enums;
using TasteTrailData.Core.Users.Models;
using TasteTrailData.Infrastructure.Filters.Dtos;
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

    private readonly UserManager<User> _userManager;

    public MenuService(IMenuRepository menuRepository, IVenueRepository venueRepository, UserManager<User> userManager)
    {
        _menuRepository = menuRepository;
        _venueRepository = venueRepository;
        _userManager = userManager;
    }

    public async Task<FilterResponseDto<Menu>> GetMenusFilteredAsync(PaginationParametersDto paginationParameters, int venueId)
    {
        if (venueId <= 0)
            throw new ArgumentException($"Invalid Venue ID: {venueId}.");

        var newFilterParameters = new FilterParameters<Menu>() {
            PageNumber = paginationParameters.PageNumber,
            PageSize = paginationParameters.PageSize,
            Specification = null,
            SearchTerm = null
        };

        var menus = await _menuRepository.GetFilteredByIdAsync(newFilterParameters, venueId);

        var totalMenus = await _menuRepository.GetCountFilteredIdAsync(newFilterParameters, venueId);
        var totalPages = (int)Math.Ceiling(totalMenus / (double)paginationParameters.PageSize);


        var filterReponse = new FilterResponseDto<Menu>() {
            CurrentPage = paginationParameters.PageNumber,
            AmountOfPages = totalPages,
            AmountOfEntities = totalMenus,
            Entities = menus
        };

        return filterReponse;
    }

    public async Task<FilterResponseDto<Menu>> GetMenusFilteredAsync(PaginationSearchParametersDto paginationSearchParameters)
    {
        var newFilterParameters = new FilterParameters<Menu>() {
            PageNumber = paginationSearchParameters.PageNumber,
            PageSize = paginationSearchParameters.PageSize,
            Specification = null,
            SearchTerm = paginationSearchParameters.SearchTerm
        };

        var menus = await _menuRepository.GetFilteredAsync(newFilterParameters);

        var totalMenus = await _menuRepository.GetCountFilteredAsync(newFilterParameters);
        var totalPages = (int)Math.Ceiling(totalMenus / (double)paginationSearchParameters.PageSize);


        var filterReponse = new FilterResponseDto<Menu>() {
            CurrentPage = paginationSearchParameters.PageNumber,
            AmountOfPages = totalPages,
            AmountOfEntities = totalMenus,
            Entities = menus
        };

        return filterReponse;
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

        if (venue.UserId != user.Id)
            throw new ForbiddenAccessException();

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

        var isAdmin = await _userManager.IsInRoleAsync(user, UserRoles.Admin.ToString());

        if (!isAdmin && menu.UserId != user.Id)
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
