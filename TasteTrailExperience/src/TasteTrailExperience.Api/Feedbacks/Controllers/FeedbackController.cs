using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasteTrailData.Api.Common.Extensions.Controllers;
using TasteTrailData.Core.Users.Models;
using TasteTrailExperience.Core.Common.Exceptions;
using TasteTrailExperience.Core.Feedbacks.Dtos;
using TasteTrailExperience.Core.Feedbacks.Services;

namespace TasteTrailExperience.Api.Feedbacks.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class FeedbackController : ControllerBase
{
    private readonly IFeedbackService _feedbackService;

    private readonly UserManager<User> _userManager;

    public FeedbackController(IFeedbackService feedbackService, UserManager<User> userManager)
    {
        _feedbackService = feedbackService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetByCountAsync(int count)
    {
        try 
        {
            var feedbacks = await _feedbackService.GetFeedbacksByCountAsync(count);

            return Ok(feedbacks);
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
             var feedback = await _feedbackService.GetFeedbackByIdAsync(id);

            if (feedback is null)
                return NotFound(id);

            return Ok(feedback);
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAsync(FeedbackCreateDto feedback)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            var feedbackId = await _feedbackService.CreateFeedbackAsync(feedback, user!);

            return Ok(feedbackId);
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
            var user = await _userManager.GetUserAsync(User);
            var feedbackId = await _feedbackService.DeleteFeedbackByIdAsync(id, user!);

            if (feedbackId is null)
                return NotFound(feedbackId);

            return Ok(feedbackId);
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
    public async Task<IActionResult> UpdateAsync(FeedbackUpdateDto feedback)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            var feedbackId = await _feedbackService.UpdateFeedbackAsync(feedback, user!);

            if (feedbackId is null)
                return NotFound(feedbackId);

            return Ok(feedbackId);
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
