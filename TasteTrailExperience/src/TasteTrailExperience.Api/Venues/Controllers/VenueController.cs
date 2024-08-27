using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasteTrailData.Api.Common.Extensions.Controllers;
using TasteTrailData.Core.Users.Models;
using TasteTrailData.Infrastructure.Filters.Dtos;
using TasteTrailExperience.Core.Common.Exceptions;
using TasteTrailExperience.Core.Venues.Dtos;
using TasteTrailExperience.Core.Venues.Services;

namespace TasteTrailExperience.Api.Venues.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class VenueController : Controller
{
    private readonly IVenueService _venueService;

    private readonly IVenueImageService _venueLogoService;

    private readonly UserManager<User> _userManager;

    public VenueController(IVenueService venueService, UserManager<User> userManager, IVenueImageService venueLogoService)
    {
        _venueService = venueService;
        _userManager = userManager;
        _venueLogoService = venueLogoService;
    }

    [HttpPost]
    public async Task<IActionResult> GetFilteredAsync(FilterParametersSearchDto filterParameters)
    {
        try 
        {
            var filterResponse = await _venueService.GetVenuesFilteredAsync(filterParameters);

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
             var venue = await _venueService.GetVenueByIdAsync(id);

            if (venue is null)
                return NotFound(id);

            return Ok(venue);
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
             var count = await _venueService.GetVenuesCountAsync();

            return Ok(count);
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAsync([FromForm] VenueCreateDto venue, IFormFile? logo)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            var venueId = await _venueService.CreateVenueAsync(venue, user!);

            await _venueLogoService.SetImageAsync(venueId, logo);

            return Ok(venueId);
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
            var venue = await _venueService.GetVenueByIdAsync(id);

            if (venue == null)
                return NotFound(id);

            await _venueLogoService.DeleteImageAsync(venue.Id);

            var user = await _userManager.GetUserAsync(User);
            var venueId = await _venueService.DeleteVenueByIdAsync(venue.Id, user!);

            return Ok(venueId);
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
    public async Task<IActionResult> UpdateAsync([FromForm] VenueUpdateDto venue, IFormFile? logo)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            var venueId = await _venueService.PutVenueAsync(venue, user!);

            if (venueId is null)
                return NotFound(venueId);

            await _venueLogoService.SetImageAsync((int)venueId, logo);

            return Ok(venueId);
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
