using System;
using Microsoft.EntityFrameworkCore;
using TasteTrailData.Core.Venues.Models;
using TasteTrailData.Infrastructure.Common.Data;
using TasteTrailExperience.Core.Filters.Models;
using TasteTrailExperience.Core.Specifications.Filters;
using TasteTrailExperience.Core.Venues.Repositories;

namespace TasteTrailExperience.Infrastructure.Venues.Repositories;

public class VenueEfCoreRepository : IVenueRepository
{
    private readonly TasteTrailDbContext _dbContext;

    public VenueEfCoreRepository(TasteTrailDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<List<Venue>> GetFilteredAsync(FilterParameters<Venue> parameters)
    {
        IQueryable<Venue> query = _dbContext.Set<Venue>();

        if (parameters.Specification is not null)
            query = parameters.Specification.Apply(query);

        query = query.Skip((parameters.PageNumber - 1) * parameters.PageSize).Take(parameters.PageSize);

        return await query.ToListAsync();
    }

    public async Task<Venue?> GetByIdAsync(int id)
    {
        return await _dbContext.Venues
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public Task<int> GetCountAsync()
    {
        return _dbContext.Venues.CountAsync();
    }

    public async Task<int> GetCountBySpecificationAsync(IFilterSpecification<Venue>? specification)
    {
        var query = _dbContext.Venues.AsQueryable();

        if (specification != null)
            query = specification.Apply(query);

        return await query.CountAsync();
    }

    public async Task<int> CreateAsync(Venue venue)
    {
        ArgumentNullException.ThrowIfNull(venue);

        await _dbContext.Venues.AddAsync(venue);
        await _dbContext.SaveChangesAsync();

        return venue.Id;
    }

    public async Task<int?> DeleteByIdAsync(int id)
    {
        var venue = await _dbContext.Venues.FindAsync(id);

        if (venue is null)
            return null;
        
        _dbContext.Venues.Remove(venue);
        await _dbContext.SaveChangesAsync();

        return id;
    }

    public async Task<int?> PutAsync(Venue venue)
    {
        ArgumentNullException.ThrowIfNull(venue);

        var venueToUpdate = await _dbContext.Venues
            .FirstOrDefaultAsync(v => v.Id == venue.Id);

        if (venueToUpdate is null)
            return null;

        venueToUpdate.Name = venue.Name;
        venueToUpdate.Address = venue.Address;
        venueToUpdate.ContactNumber = venue.ContactNumber;
        venueToUpdate.Email = venue.Email;
        venueToUpdate.LogoUrlPath = venue.LogoUrlPath;
        venueToUpdate.AveragePrice = venue.AveragePrice;

        await _dbContext.SaveChangesAsync();

        return venueToUpdate.Id;
    }

    public async Task<Venue?> GetAsNoTrackingAsync(int id)
    {
        return await _dbContext.Venues
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task PatchLogoUrlPathAsync(Venue venue, string logoUrlPath)
    {
        ArgumentNullException.ThrowIfNull(venue);

        if (string.IsNullOrWhiteSpace(logoUrlPath))
            throw new ArgumentException("Logo URL path cannot be null or empty.", nameof(logoUrlPath));

        venue.LogoUrlPath = logoUrlPath;
        _dbContext.Venues.Update(venue);

        await _dbContext.SaveChangesAsync();
    }
}
