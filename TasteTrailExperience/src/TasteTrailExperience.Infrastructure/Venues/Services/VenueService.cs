using Microsoft.AspNetCore.Http;
using TasteTrailData.Core.Venues.Models;
using TasteTrailExperience.Core.Venues.Dtos;
using TasteTrailExperience.Core.Venues.Repositories;
using TasteTrailExperience.Core.Venues.Services;

namespace TasteTrailExperience.Infrastructure.Venues.Services;

public class VenueService : IVenueService
{
    private readonly IVenueRepository _venueRepository;

    public VenueService(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }

    public async Task<List<VenueGetDto>> GetVenuesByCountAsync(int count)
    {
        var venues = await _venueRepository.GetByCountAsync(count);

        var venueDtos = venues.Select(venue => new VenueGetDto
        {
            Id = venue.Id,
            Name = venue.Name,
            Address = venue.Address,
            Description = venue.Description,
            Email = venue.Email,
            ContactNumber = venue.ContactNumber,
            AveragePrice = venue.AveragePrice,
            OverallRating = venue.OverallRating,
            Menus = venue.Menus,
            Feedbacks = venue.Feedbacks
        }).ToList();

        return venueDtos;
    }

    public async Task<VenueGetDto?> GetVenueByIdAsync(int id)
    {
        var venue = await _venueRepository.GetByIdAsync(id);

        if (venue is null)
            return null;

        var venueDto = new VenueGetDto() 
        {
            Id = venue.Id,
            Name = venue.Name,
            Address = venue.Address,
            Description = venue.Description,
            Email = venue.Email,
            ContactNumber = venue.ContactNumber,
            AveragePrice = venue.AveragePrice,
            OverallRating = venue.OverallRating,
            Menus = venue.Menus,
            Feedbacks = venue.Feedbacks
        };

        return venueDto;
    }

    public async Task<int> CreateVenueAsync(VenueCreateDto venue)
    {
        var newVenue = new Venue() {
            Name = venue.Name,
            Address = venue.Address,
            Description = venue.Description,
            Email = venue.Email,
            ContactNumber = venue.ContactNumber,
            AveragePrice = venue.AveragePrice
        };

        var venueId = await _venueRepository.CreateAsync(newVenue);

        return venueId;
    }

    public async Task<int?> DeleteVenueByIdAsync(int id)
    {
        var venueId = await _venueRepository.DeleteByIdAsync(id);

        return venueId;
    }

    public async Task<int?> PutVenueAsync(VenueUpdateDto venue)
    {
        var newVenue = new Venue() {
            Id = venue.Id,
            Name = venue.Name,
            Address = venue.Address,
            Description = venue.Description,
            Email = venue.Email,
            ContactNumber = venue.ContactNumber,
            AveragePrice = venue.AveragePrice
        };

        var venueId = await _venueRepository.PutAsync(newVenue);

        return venueId;
    }

    public Task<string> SetVenueLogo(Venue venue, IFormFile? logo)
    {
        throw new NotImplementedException();
    }

    public Task<string> DeleteVenueLogoAsync(int venueId)
    {
        throw new NotImplementedException();
    }
}
