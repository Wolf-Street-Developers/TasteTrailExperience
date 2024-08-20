using Microsoft.AspNetCore.Http;
using TasteTrailData.Core.Venues.Models;

namespace TasteTrailExperience.Core.Venues.Services;

public interface IVenueLogoService
{
    Task<string> SetVenueLogo(int venueId, IFormFile? logo);

    Task<string> DeleteVenueLogoAsync(int venueId);
}
