using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasteTrailData.Api.Common.Extensions.Controllers;
using TasteTrailData.Core.Users.Models;
using TasteTrailExperience.Core.Common.Exceptions;
using TasteTrailExperience.Core.FeedbackLikes.Dtos;
using TasteTrailExperience.Core.FeedbackLikes.Services;

namespace TasteTrailExperience.Api.FeedbackLikes.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class FeedbackLikeController : ControllerBase
{
    private readonly IFeedbackLikeService _feedbackLikeService;
    private readonly UserManager<User> _userManager;

    public FeedbackLikeController(IFeedbackLikeService feedbackLikeService, UserManager<User> userManager)
    {
        _feedbackLikeService = feedbackLikeService;
        _userManager = userManager;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAsync([FromForm] FeedbackLikeCreateDto feedbackLike, IFormFile? logo)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            var feedbackLikeId = await _feedbackLikeService.CreateFeedbackLikeAsync(feedbackLike, user!);

            return Ok(feedbackLikeId);
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
            var feedbackLikeId = await _feedbackLikeService.DeleteFeedbackLikeByIdAsync(id, user!);

            if (feedbackLikeId is null)
                return NotFound(feedbackLikeId);

            return Ok(feedbackLikeId);
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
