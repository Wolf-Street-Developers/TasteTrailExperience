using Microsoft.AspNetCore.Mvc;
using TasteTrailData.Api.Common.Extensions.Controllers;
using TasteTrailExperience.Core.MenuItems.Dtos;
using TasteTrailExperience.Core.MenuItems.Services;

namespace TasteTrailExperience.Api.MenuItem.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class MenuItemController : ControllerBase
{
    private readonly IMenuItemService _menuItemService;

    public MenuItemController(IMenuItemService menuItemService)
    {
        _menuItemService = menuItemService;
    }

    [HttpGet]
    public async Task<IActionResult> GetByCountAsync(int count)
    {
        try 
        {
            var menuItems = await _menuItemService.GetMenuItemsByCountAsync(count);

            return Ok(menuItems);
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
    public async Task<IActionResult> CreateAsync(MenuItemCreateDto menu)
    {
        try
        {
            var menuItemId = await _menuItemService.CreateMenuItemAsync(menu);

            return Ok(menuItemId);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteByIdAsync(int id)
    {
        try
        {
            var menuItemId = await _menuItemService.DeleteMenuItemByIdAsync(id);

            if (menuItemId is null)
                return NotFound(menuItemId);

            return Ok(menuItemId);
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(MenuItemUpdateDto venue)
    {
        try
        {
            var menuItemId = await _menuItemService.PutMenuItemAsync(venue);

            if (menuItemId is null)
                return NotFound(menuItemId);

            return Ok(menuItemId);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }
}
