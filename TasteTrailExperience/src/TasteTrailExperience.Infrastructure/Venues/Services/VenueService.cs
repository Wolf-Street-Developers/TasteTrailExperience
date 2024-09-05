using Microsoft.AspNetCore.Identity;
using TasteTrailData.Core.Filters.Specifications;
using TasteTrailData.Core.Users.Models;
using TasteTrailData.Core.Venues.Models;
using TasteTrailData.Infrastructure.Filters.Dtos;
using TasteTrailExperience.Core.Common.Exceptions;
using TasteTrailExperience.Core.Roles;
using TasteTrailExperience.Core.Venues.Dtos;
using TasteTrailExperience.Core.Venues.Repositories;
using TasteTrailExperience.Core.Venues.Services;
using TasteTrailExperience.Infrastructure.Venues.Factories;

namespace TasteTrailExperience.Infrastructure.Venues.Services;

public class VenueService : IVenueService
{
    private readonly IVenueRepository _venueRepository;

    private readonly UserManager<User> _userManager;

    public VenueService(IVenueRepository venueRepository, UserManager<User> userManager)
    {
        _venueRepository = venueRepository;
        _userManager = userManager;
    }

    public async Task<FilterResponseDto<Venue>> GetVenuesFilteredAsync(FilterParametersSearchDto filterParameters)
    {
        var newFilterParameters = new FilterParameters<Venue>() {
            PageNumber = filterParameters.PageNumber,
            PageSize = filterParameters.PageSize,
            Specification = VenueFilterFactory.CreateFilter(filterParameters.Type),
            SearchTerm = filterParameters.SearchTerm
        };

        var venues = await _venueRepository.GetFilteredAsync(newFilterParameters);

        var totalVenues = await _venueRepository.GetCountFilteredAsync(newFilterParameters);
        var totalPages = (int)Math.Ceiling(totalVenues / (double)filterParameters.PageSize);

        var filterReponse = new FilterResponseDto<Venue>() {
            CurrentPage = filterParameters.PageNumber,
            AmountOfPages = totalPages,
            AmountOfEntities = totalVenues,
            Entities = venues,
        };

        return filterReponse;
    }

    public async Task<Venue?> GetVenueByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException($"Invalid ID value: {id}.");

        var venue = await _venueRepository.GetByIdAsync(id);
        return venue;
    }

    public Task<int> GetVenuesCountAsync()
    {
        return _venueRepository.GetCountFilteredAsync(null);
    }

    public async Task<int> CreateVenueAsync(VenueCreateDto venue, User user)
    {
        var newVenue = new Venue() {
            Name = venue.Name,
            Address = venue.Address,
            Longtitude = venue.Longtitude,
            Latitude = venue.Latitude,
            Description = venue.Description,
            Email = venue.Email,
            ContactNumber = venue.ContactNumber,
            AveragePrice = venue.AveragePrice,
            UserId = user.Id,
            CreationDate = DateTime.UtcNow,
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

        var isAdmin = await _userManager.IsInRoleAsync(user, UserRoles.Admin.ToString());

        if (!isAdmin && venue.UserId != user.Id)
            throw new ForbiddenAccessException();


        var venueId = await _venueRepository.DeleteByIdAsync(id);

        return venueId;
    }

    public async Task<int?> PutVenueAsync(VenueUpdateDto venue, User user)
    {
        var venueToUpdate = await _venueRepository.GetAsNoTrackingAsync(venue.Id);

        if (venueToUpdate is null)
            return null;

        var isAdmin = await _userManager.IsInRoleAsync(user, UserRoles.Admin.ToString());

        if (!isAdmin && venueToUpdate.UserId != user.Id)
            throw new ForbiddenAccessException();

        var updatedVenue = new Venue() {
            Id = venue.Id,
            Name = venue.Name,
            Address = venue.Address,
            Longtitude = venue.Longtitude,
            Latitude = venue.Latitude,
            Description = venue.Description,
            Email = venue.Email,
            ContactNumber = venue.ContactNumber,
            AveragePrice = venue.AveragePrice,
            UserId = user.Id,
            CreationDate = DateTime.UtcNow,
        };

        var venueId = await _venueRepository.PutAsync(updatedVenue);

        return venueId;
    }
}
