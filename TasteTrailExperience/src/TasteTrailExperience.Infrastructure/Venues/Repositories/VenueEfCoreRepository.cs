using Microsoft.EntityFrameworkCore;
using TasteTrailData.Core.Venues.Models;
using TasteTrailData.Infrastructure.Common.Data;
using TasteTrailExperience.Core.Venues.Repositories;

namespace TasteTrailExperience.Infrastructure.Venues.Repositories;

public class VenueEfCoreRepository : IVenueRepository
{
    private readonly TasteTrailDbContext _dbContext;

    public VenueEfCoreRepository(TasteTrailDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<List<Venue>> GetByCountAsync(int count)
    {
        return await _dbContext.Venues
            .Include(v => v.Menus)
            .Include(v => v.Feedbacks)
            .Take(count)
            .ToListAsync();
    }

    public async Task<Venue?> GetByIdAsync(int id)
    {
        return await _dbContext.Venues
            .Include(v => v.Menus)
            .Include(v => v.Feedbacks)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<int> CreateAsync(Venue venue)
    {
        ArgumentNullException.ThrowIfNull(venue);

        await _dbContext.Venues.AddAsync(venue);
        await _dbContext.SaveChangesAsync();

        return venue.Id;
    }

    public async Task<int?> DeleteByIdAsync(int? id)
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
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.Id == venue.Id);

        if (venueToUpdate is null)
            return null;

        _dbContext.Venues.Update(venue);
        await _dbContext.SaveChangesAsync();

        return venueToUpdate.Id;
    }
}