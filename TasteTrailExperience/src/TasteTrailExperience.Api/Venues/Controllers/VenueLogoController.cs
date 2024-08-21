using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasteTrailData.Api.Common.Extensions.Controllers;
using TasteTrailData.Core.Users.Models;
using TasteTrailExperience.Core.Common.Exceptions;
using TasteTrailExperience.Core.Venues.Services;

namespace TasteTrailExperience.Api.Venues.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class VenueLogoController : ControllerBase
{
    private readonly IVenueLogoService _venueLogoService;

    private readonly UserManager<User> _userManager;

    public VenueLogoController(IVenueLogoService venueLogoService, UserManager<User> userManager)
    {
        _venueLogoService = venueLogoService;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(IFormFile logo, int venueId)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            // var venueId = await _venueService.CreateVenueAsync(venue, user!);

            // // Setting default logo
            // await _venueLogoService.SetVenueLogo(venueId, null);

            return Ok();
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
