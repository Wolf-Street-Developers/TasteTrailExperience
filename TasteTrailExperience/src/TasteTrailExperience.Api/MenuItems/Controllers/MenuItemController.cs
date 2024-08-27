using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasteTrailData.Api.Common.Extensions.Controllers;
using TasteTrailData.Core.Users.Models;
using TasteTrailData.Infrastructure.Filters.Dtos;
using TasteTrailExperience.Core.Common.Exceptions;
using TasteTrailExperience.Core.MenuItems.Dtos;
using TasteTrailExperience.Core.MenuItems.Services;

namespace TasteTrailExperience.Api.MenuItems.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class MenuItemController : ControllerBase
{
    private readonly IMenuItemService _menuItemService;

    private readonly UserManager<User> _userManager;

    public MenuItemController(IMenuItemService menuItemService, UserManager<User> userManager)
    {
        _menuItemService = menuItemService;
        _userManager = userManager;
    }

    [HttpPost("{menuId}")]
    public async Task<IActionResult> GetFilteredAsync(FilterParametersSearchDto filterParameters, int menuId)
    {
        try 
        {
            var filterResponse = await _menuItemService.GetMenuItemsFilteredAsync(filterParameters, menuId);

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
    [Authorize]
    public async Task<IActionResult> CreateAsync(MenuItemCreateDto menu)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            var menuItemId = await _menuItemService.CreateMenuItemAsync(menu, user!);

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
    [Authorize]
    public async Task<IActionResult> DeleteByIdAsync(int id)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            var menuItemId = await _menuItemService.DeleteMenuItemByIdAsync(id, user!);

            if (menuItemId is null)
                return NotFound(menuItemId);

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
    [Authorize]
    public async Task<IActionResult> UpdateAsync(MenuItemUpdateDto venue)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            var menuItemId = await _menuItemService.PutMenuItemAsync(venue, user!);

            if (menuItemId is null)
                return NotFound(menuItemId);

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
