using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasteTrailData.Api.Common.Extensions.Controllers;
using TasteTrailData.Core.Filters.Enums;
using TasteTrailData.Core.Users.Models;
using TasteTrailData.Infrastructure.Filters.Dtos;
using TasteTrailExperience.Core.Common.Exceptions;
using TasteTrailExperience.Core.Menus.Dtos;
using TasteTrailExperience.Core.Menus.Services;

namespace TasteTrailExperience.Api.Menus.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class MenuController : ControllerBase
{
    private readonly IMenuService _menuService;

    private readonly UserManager<User> _userManager;

    public MenuController(IMenuService menuService, UserManager<User> userManager)
    {
        _menuService = menuService;
        _userManager = userManager;
    }

    [HttpPost("{venueId}")]
    public async Task<IActionResult> GetFilteredAsync(FilterParametersPaginationDto filterParameters, int venueId)
    {
        try 
        {
            var filterResponse = await _menuService.GetMenusFilteredAsync(filterParameters, venueId);

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
    public async Task<IActionResult> CreateAsync([FromForm] MenuCreateDto menu, IFormFile? logo)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            var menuId = await _menuService.CreateMenuAsync(menu, user!);

            return Ok(menuId);
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
            var menuId = await _menuService.DeleteMenuByIdAsync(id, user!);

            if (menuId is null)
                return NotFound(menuId);

            return Ok(menuId);
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
    public async Task<IActionResult> UpdateAsync([FromForm] MenuUpdateDto venue, IFormFile? logo)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            var menuId = await _menuService.PutMenuAsync(venue, user!);

            if (menuId is null)
                return NotFound(menuId);

            return Ok(menuId);
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
