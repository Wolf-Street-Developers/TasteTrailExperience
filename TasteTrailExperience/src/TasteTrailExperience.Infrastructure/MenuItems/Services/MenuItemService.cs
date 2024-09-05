using TasteTrailData.Core.Filters.Specifications;
using TasteTrailData.Core.MenuItems.Models;
using TasteTrailData.Core.Users.Models;
using TasteTrailData.Infrastructure.Filters.Dtos;
using TasteTrailExperience.Core.Common.Exceptions;
using TasteTrailExperience.Core.MenuItemLikes.Repositories;
using TasteTrailExperience.Core.MenuItems.Dtos;
using TasteTrailExperience.Core.MenuItems.Repositories;
using TasteTrailExperience.Core.MenuItems.Services;
using TasteTrailExperience.Core.Menus.Repositories;
using TasteTrailExperience.Infrastructure.MenuItems.Factories;

namespace TasteTrailExperience.Infrastructure.MenuItems.Services;

public class MenuItemService : IMenuItemService
{
    private readonly IMenuItemRepository _menuItemRepository;

    private readonly IMenuRepository _menuRepository;

    private readonly IMenuItemLikeRepository _menuItemLikeRepository;

    public MenuItemService(IMenuItemRepository menuItemRepository, IMenuRepository menuRepository, IMenuItemLikeRepository menuItemLikeRepository)
    {
        _menuItemRepository = menuItemRepository;
        _menuRepository = menuRepository;
        _menuItemLikeRepository = menuItemLikeRepository;
    }

    public async Task<FilterResponseDto<MenuItemGetDto>> GetMenuItemsFilteredAsync(FilterParametersSearchDto filterParameters, int menuId, User? authenticatedUser)
    {
        if (menuId <= 0)
            throw new ArgumentException($"Invalid Venue ID: {menuId}.");

        var newFilterParameters = new FilterParameters<MenuItem>() {
            PageNumber = filterParameters.PageNumber,
            PageSize = filterParameters.PageSize,
            Specification = MenuItemFilterFactory.CreateFilter(filterParameters.Type),
            SearchTerm = filterParameters.SearchTerm
        };

        var menuItems = await _menuItemRepository.GetFilteredByIdAsync(newFilterParameters, menuId);
        var menuItemDtos = new List<MenuItemGetDto>();

        List<int>? likedMenuItemIds = null;

        if (authenticatedUser is not null)
            likedMenuItemIds = await _menuItemLikeRepository.GetLikedMenuItemIds(authenticatedUser.Id);

        foreach (var menuItem in menuItems)
        {
            var isLiked = likedMenuItemIds is not null && likedMenuItemIds.Any(id => id == menuItem.Id);

            var menuItemDto = new MenuItemGetDto
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                Description = menuItem.Description,
                ImageUrlPath = menuItem.ImageUrlPath,
                Price = menuItem.Price!,
                Likes = menuItem.Likes,
                MenuId = menuItem.MenuId,
                UserId = menuItem.UserId,
                IsLiked = isLiked
            };

            menuItemDtos.Add(menuItemDto);
        }

        var totalMenuItems = await _menuItemRepository.GetCountFilteredIdAsync(newFilterParameters, menuId);
        var totalPages = (int)Math.Ceiling(totalMenuItems / (double)filterParameters.PageSize);


        var filterReponse = new FilterResponseDto<MenuItemGetDto>() {
            CurrentPage = filterParameters.PageNumber,
            AmountOfPages = totalPages,
            AmountOfEntities = totalMenuItems,
            Entities = menuItemDtos
        };

        return filterReponse;
    }

    
    public async Task<FilterResponseDto<MenuItemGetDto>> GetMenuItemsFilteredAsync(FilterParametersSearchDto filterParameters, User? authenticatedUser)
    {
        var newFilterParameters = new FilterParameters<MenuItem>() {
            PageNumber = filterParameters.PageNumber,
            PageSize = filterParameters.PageSize,
            Specification = MenuItemFilterFactory.CreateFilter(filterParameters.Type),
            SearchTerm = filterParameters.SearchTerm
        };

        var menuItems = await _menuItemRepository.GetFilteredAsync(newFilterParameters);
        var menuItemDtos = new List<MenuItemGetDto>();

        List<int>? likedMenuItemIds = null;

        if (authenticatedUser is not null)
            likedMenuItemIds = await _menuItemLikeRepository.GetLikedMenuItemIds(authenticatedUser.Id);

        foreach (var menuItem in menuItems)
        {
            var isLiked = likedMenuItemIds is not null && likedMenuItemIds.Any(id => id == menuItem.Id);

            var menuItemDto = new MenuItemGetDto
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                Description = menuItem.Description,
                ImageUrlPath = menuItem.ImageUrlPath,
                Price = menuItem.Price!,
                Likes = menuItem.Likes,
                MenuId = menuItem.MenuId,
                UserId = menuItem.UserId,
                IsLiked = isLiked
            };

            menuItemDtos.Add(menuItemDto);
        }

        var totalMenuItems = await _menuItemRepository.GetCountFilteredAsync(newFilterParameters);
        var totalPages = (int)Math.Ceiling(totalMenuItems / (double)filterParameters.PageSize);


        var filterReponse = new FilterResponseDto<MenuItemGetDto>() {
            CurrentPage = filterParameters.PageNumber,
            AmountOfPages = totalPages,
            AmountOfEntities = totalMenuItems,
            Entities = menuItemDtos
        };

        return filterReponse;
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
            UserId = user.Id,
            MenuId = menuItemToUpdate.MenuId
        };

        var menuItemId = await _menuItemRepository.PutAsync(updatedMenuItem);

        return menuItemId;
    }
}
