using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasteTrailData.Api.Common.Extensions.Controllers;
using TasteTrailData.Core.Roles.Enums;
using TasteTrailData.Core.Users.Models;
using TasteTrailData.Infrastructure.Filters.Dtos;
using TasteTrailExperience.Core.Common.Exceptions;
using TasteTrailExperience.Core.MenuItems.Dtos;
using TasteTrailExperience.Core.MenuItems.Services;
using TasteTrailExperience.Infrastructure.MenuItems.Managers;

namespace TasteTrailExperience.Api.MenuItems.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class MenuItemController : ControllerBase
{
    private readonly IMenuItemService _menuItemService;

    private readonly MenuItemImageManager _menuItemImageManager;

    private readonly UserManager<User> _userManager;

    public MenuItemController(IMenuItemService menuItemService, UserManager<User> userManager, MenuItemImageManager menuItemImageManager)
    {
        _menuItemService = menuItemService;
        _userManager = userManager;
        _menuItemImageManager = menuItemImageManager;
    }

    [HttpPost("{menuId}")]
    public async Task<IActionResult> GetFilteredAsync(FilterParametersSearchDto filterParameters, int menuId)
    {
        try 
        {
            var user = await _userManager.GetUserAsync(User);
            var filterResponse = await _menuItemService.GetMenuItemsFilteredAsync(filterParameters, menuId, user);

            return Ok(filterResponse);
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> GetFilteredAsync(FilterParametersSearchDto filterParameters)
    {
        try 
        {
            var user = await _userManager.GetUserAsync(User);
            var filterResponse = await _menuItemService.GetMenuItemsFilteredAsync(filterParameters, user);

            return Ok(filterResponse);
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
             var menuItem = await _menuItemService.GetMenuItemByIdAsync(id);

            if (menuItem is null)
                return NotFound(id);

            return Ok(menuItem);
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Roles = $"{nameof(UserRoles.Admin)},{nameof(UserRoles.Owner)}")]
    public async Task<IActionResult> CreateAsync([FromForm] MenuItemCreateDto menu, IFormFile? logo)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            var menuItemId = await _menuItemService.CreateMenuItemAsync(menu, user!);

            await _menuItemImageManager.SetImageAsync(menuItemId, logo);

            return Ok(menuItemId);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ForbiddenAccessException)
        {
            return Forbid();
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }

    [HttpDelete]
    [Authorize(Roles = $"{nameof(UserRoles.Admin)},{nameof(UserRoles.Owner)}")]
    public async Task<IActionResult> DeleteByIdAsync(int id)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            var menuItem = await _menuItemService.GetMenuItemByIdAsync(id);

            if (menuItem is null)
                return NotFound(id);

            await _menuItemImageManager.DeleteImageAsync(menuItem.Id);
            var menuItemId = await _menuItemService.DeleteMenuItemByIdAsync(id, user!);

            return Ok(menuItemId);
        }
        catch (ForbiddenAccessException)
        {
            return Forbid();
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }

    [HttpPut]
    [Authorize(Roles = $"{nameof(UserRoles.Admin)},{nameof(UserRoles.Owner)}")]
    public async Task<IActionResult> UpdateAsync([FromForm] MenuItemUpdateDto venue, IFormFile? logo)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            var menuItemId = await _menuItemService.PutMenuItemAsync(venue, user!);

            if (menuItemId is null)
                return NotFound(menuItemId);

            await _menuItemImageManager.SetImageAsync((int)menuItemId, logo);

            return Ok(menuItemId);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ForbiddenAccessException)
        {
            return Forbid();
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }
}
