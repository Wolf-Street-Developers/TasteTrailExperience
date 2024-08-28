using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasteTrailData.Api.Common.Extensions.Controllers;
using TasteTrailData.Core.Users.Models;
using TasteTrailExperience.Core.Common.Exceptions;
using TasteTrailExperience.Core.MenuItemLikes.Dtos;
using TasteTrailExperience.Core.MenuItemLikes.Services;

namespace TasteTrailExperience.Api.MenuItemLikes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuItemLikeController : ControllerBase
{
    private readonly IMenuItemLikeService _menuItemLikeService;
    private readonly UserManager<User> _userManager;

    public MenuItemLikeController(IMenuItemLikeService menuItemLikeService, UserManager<User> userManager)
    {
        _menuItemLikeService = menuItemLikeService;
        _userManager = userManager;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAsync([FromForm] MenuItemLikeCreateDto menuItemLike)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            var menuItemLikeId = await _menuItemLikeService.CreateMenuItemLikeAsync(menuItemLike, user!);

            return Ok(menuItemLikeId);
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

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteByIdAsync(int id)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            var menuItemLikeId = await _menuItemLikeService.DeleteMenuItemLikeByIdAsync(id, user!);

            if (menuItemLikeId is null)
                return NotFound(menuItemLikeId);

            return Ok(menuItemLikeId);
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
