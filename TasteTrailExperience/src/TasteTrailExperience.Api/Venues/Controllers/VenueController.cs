using Microsoft.AspNetCore.Mvc;
using TasteTrailData.Core.Venues.Models;
using TasteTrailExperience.Api.Common.Extensions.Controller;
using TasteTrailExperience.Core.Venues.Dtos;
using TasteTrailExperience.Core.Venues.Services;

namespace TasteTrailExperience.Api.Venues.Controllers;

[ApiController]
[Route("/api/[controller]/[action]")]
public class VenueController : Controller
{
    private readonly IVenueService _venueService;

    public VenueController(IVenueService venueService)
    {
        _venueService = venueService;
    }

    [HttpGet]
    public async Task<IActionResult> GetByCountAsync(int count)
    {
        try 
        {
            var venues = await _venueService.GetVenuesByCountAsync(count);

            return Ok(venues);
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

    [HttpPost]
    public async Task<IActionResult> CreateAsync(VenueCreateDto venue)
    {
        try
        {
            var venueId = await _venueService.CreateVenueAsync(venue);

            return Ok(venueId);
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
            var venueId = await _venueService.DeleteVenueByIdAsync(id);

            if (venueId is null)
                return NotFound(venueId);

            return Ok(venueId);
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(VenueUpdateDto venue)
    {
        try
        {
            var venueId = await _venueService.PutVenueAsync(venue);

            if (venueId is null)
                return NotFound(venueId);

            return Ok(venueId);
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex.Message);
        }
    }
}
