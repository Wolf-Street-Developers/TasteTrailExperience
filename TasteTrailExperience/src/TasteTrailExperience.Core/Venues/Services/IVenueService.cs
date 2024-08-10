using Microsoft.AspNetCore.Http;
using TasteTrailData.Core.Venues.Models;

namespace TasteTrailExperience.Core.Venues.Services;

public interface IVenueService
{
    Task<IEnumerable<Venue>> GetVenuesByCountAsync(int count);

    Task<Venue> GetVenueByIdAsync(int id);

    Task<int> CreateVenueAsync(Venue venue, IFormFile? logo);

    Task<int> DeleteVenueByIdAsync(int id);
    
    Task<int> PutVenueAsync(Venue entity);

    Task<string> SetVenueLogo(Venue venue, IFormFile? logo);

    Task<string> DeleteVenueLogoAsync(int venueId);
}
