using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasteTrailData.Api.Common.Extensions.Controllers;
using TasteTrailExperience.Core.Menus.Dtos;
using TasteTrailExperience.Core.Menus.Services;

namespace TasteTrailExperience.Api.Menus.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class MenuController : ControllerBase
{
    private readonly IMenuService _menuService;

    public MenuController(IMenuService menuService)
    {
        _menuService = menuService;
    }

    [HttpGet]
    public async Task<IActionResult> GetByCountAsync(int count)
    {
        try 
        {
            var menus = await _menuService.GetMenusByCountAsync(count);

            return Ok(menus);
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
             var menu = await _menuService.GetMenuByIdAsync(id);

            if (menu is null)
                return NotFound(id);

            return Ok(menu);
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAsync(MenuCreateDto menu)
    {
        try
        {
            var menuId = await _menuService.CreateMenuAsync(menu);

            return Ok(menuId);
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
    [Authorize]
    public async Task<IActionResult> DeleteByIdAsync(int id)
    {
        try
        {
            var menuId = await _menuService.DeleteMenuByIdAsync(id);

            if (menuId is null)
                return NotFound(menuId);

            return Ok(menuId);
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateAsync(MenuUpdateDto venue)
    {
        try
        {
            var menuId = await _menuService.PutMenuAsync(venue);

            if (menuId is null)
                return NotFound(menuId);

            return Ok(menuId);
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
