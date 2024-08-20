using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using TasteTrailData.Core.Users.Models;
using TasteTrailData.Core.Venues.Models;
using TasteTrailExperience.Core.Common.Exceptions;
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

    public async Task<List<VenueGetByCountDto>> GetVenuesFromToAsync(int from, int to)
    {
        if (from <= 0 || to <= 0 || from > to)
            throw new ArgumentException("Invalid 'from' and/or 'to' values.");

        var venues = await _venueRepository.GetFromToAsync(from, to);

        var venueDtos = venues.Select(venue => new VenueGetByCountDto
        {
            Id = venue.Id,
            Name = venue.Name,
            Address = venue.Address,
            Description = venue.Description,
            Email = venue.Email,
            ContactNumber = venue.ContactNumber,
            LogoUrlPath = venue.LogoUrlPath,
            AveragePrice = venue.AveragePrice,
            OverallRating = venue.OverallRating,
            UserId = venue.UserId
        }).ToList();


        return venueDtos;
    }

    public async Task<Venue?> GetVenueByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException($"Invalid ID value: {id}.");

        var venue = await _venueRepository.GetByIdAsync(id);

        return venue;
    }

    public async Task<int> CreateVenueAsync(VenueCreateDto venue, User user)
    {
        var newVenue = new Venue() {
            Name = venue.Name,
            Address = venue.Address,
            Description = venue.Description,
            Email = venue.Email,
            ContactNumber = venue.ContactNumber,
            AveragePrice = venue.AveragePrice,
            UserId = user.Id
        };

        var venueId = await _venueRepository.CreateAsync(newVenue);

        return venueId;
    }

    public async Task<int?> DeleteVenueByIdAsync(int id, User user)
    {
        if (id <= 0)
            throw new ArgumentException($"Invalid ID value: {id}.");

        var venue = await _venueRepository.GetAsNoTrackingAsync(id);

        if (venue is null)
            return null;

        if (venue.UserId != user.Id) 
            throw new ForbiddenAccessException();

        var venueId = await _venueRepository.DeleteByIdAsync(id);

        return venueId;
    }

    public async Task<int?> PutVenueAsync(VenueUpdateDto venue, User user)
    {
        var venueToUpdate = await _venueRepository.GetAsNoTrackingAsync(venue.Id);

        if (venueToUpdate is null)
            return null;

        if (venueToUpdate.UserId != user.Id)
            throw new ForbiddenAccessException();

        var updatedVenue = new Venue() {
            Id = venue.Id,
            Name = venue.Name,
            Address = venue.Address,
            Description = venue.Description,
            Email = venue.Email,
            ContactNumber = venue.ContactNumber,
            AveragePrice = venue.AveragePrice,
            UserId = user.Id
        };

        var venueId = await _venueRepository.PutAsync(updatedVenue);

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
