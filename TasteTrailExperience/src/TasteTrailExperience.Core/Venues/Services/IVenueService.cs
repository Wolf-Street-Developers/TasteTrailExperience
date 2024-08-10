using Microsoft.AspNetCore.Http;
using TasteTrailData.Core.Venues.Models;
using TasteTrailExperience.Core.Venues.Dtos;

namespace TasteTrailExperience.Core.Venues.Services;

public interface IVenueService
{
    Task<List<VenueGetDto>> GetVenuesByCountAsync(int count);

    Task<VenueGetDto?> GetVenueByIdAsync(int id);

    Task<int> CreateVenueAsync(VenueCreateDto venue);

    Task<int?> DeleteVenueByIdAsync(int id);
    
    Task<int?> PutVenueAsync(VenueUpdateDto venue);

    Task<string> SetVenueLogo(Venue venue, IFormFile? logo);

    Task<string> DeleteVenueLogoAsync(int venueId);
}
