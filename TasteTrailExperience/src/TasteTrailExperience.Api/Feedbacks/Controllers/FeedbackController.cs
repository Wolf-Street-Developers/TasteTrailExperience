using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasteTrailData.Api.Common.Extensions.Controllers;
using TasteTrailData.Core.Users.Models;
using TasteTrailExperience.Core.Common.Exceptions;
using TasteTrailExperience.Core.Feedbacks.Dtos;
using TasteTrailExperience.Core.Feedbacks.Services;
using TasteTrailExperience.Core.Filters.Dtos;

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

    [HttpGet("{venueId}")]
    public async Task<IActionResult> GetFilteredAsync(FilterParametersDto filterParameters, int venueId)
    {
        try 
        {
            var filterResponse = await _feedbackService.GetFeedbacksFilteredAsync(filterParameters, venueId);

            return Ok(filterResponse);
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }

    [HttpGet("{id}")]
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

    [HttpGet]
    public async Task<IActionResult> GetCountAsync()
    {
        try 
        {
            var count = await _feedbackService.GetFeedbacksCountAsync();

            return Ok(count);
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAsync([FromBody] FeedbackCreateDto feedback)
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

    [HttpDelete("{id}")]
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
    public async Task<IActionResult> UpdateAsync([FromBody] FeedbackUpdateDto feedback)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            var feedbackId = await _feedbackService.PutFeedbackAsync(feedback, user!);

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
