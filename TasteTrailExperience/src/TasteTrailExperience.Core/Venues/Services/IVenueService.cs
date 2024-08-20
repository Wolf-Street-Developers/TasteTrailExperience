using Microsoft.AspNetCore.Http;
using TasteTrailData.Core.Users.Models;
using TasteTrailData.Core.Venues.Models;
using TasteTrailExperience.Core.Venues.Dtos;

namespace TasteTrailExperience.Core.Venues.Services;

public interface IVenueService
{
    Task<List<Venue>> GetVenuesFromToAsync(int from, int to);

    Task<Venue?> GetVenueByIdAsync(int id);

    Task<int> GetVenuesCountAsync();

    Task<int> CreateVenueAsync(VenueCreateDto venue, User user);

    Task<int?> DeleteVenueByIdAsync(int id, User user);
    
    Task<int?> PutVenueAsync(VenueUpdateDto venue, User user);

    Task<string> SetVenueLogo(Venue venue, IFormFile? logo);

    Task<string> DeleteVenueLogoAsync(int venueId);
}
