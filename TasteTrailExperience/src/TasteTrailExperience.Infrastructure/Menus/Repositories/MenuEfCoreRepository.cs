using Microsoft.EntityFrameworkCore;
using TasteTrailData.Core.Menus.Models;
using TasteTrailData.Infrastructure.Common.Data;
using TasteTrailExperience.Core.Menus.Repositories;

namespace TasteTrailExperience.Infrastructure.Menus.Repositories;

public class MenuEfCoreRepository : IMenuRepository
{
    private readonly TasteTrailDbContext _dbContext;

    public MenuEfCoreRepository(TasteTrailDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<List<Menu>> GetByCountAsync(int count)
    {
        return await _dbContext.Menus
            .Take(count)
            .ToListAsync();
    }

    public async Task<Menu?> GetByIdAsync(int id)
    {
        return await _dbContext.Menus
            .Include(m => m.MenuItems)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<int> CreateAsync(Menu menu)
    {
        ArgumentNullException.ThrowIfNull(menu);

        var venue = await _dbContext.Venues.FirstOrDefaultAsync(v => v.Id == menu.VenueId) ?? 
            throw new ArgumentException($"Venue by ID: {menu.VenueId} not found.");

        await _dbContext.Menus.AddAsync(menu);
        await _dbContext.SaveChangesAsync();

        return menu.Id;
    }

    public async Task<int?> DeleteByIdAsync(int id)
    {
        var menu = await _dbContext.Menus.FindAsync(id);

        if (menu is null)
            return null;
        
        _dbContext.Menus.Remove(menu);
        await _dbContext.SaveChangesAsync();

        return id;
    }

    public async Task<int?> PutAsync(Menu menu)
    {
        ArgumentNullException.ThrowIfNull(menu);

        var menuToUpdate = await _dbContext.Menus
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == menu.Id);

        if (menuToUpdate is null)
            return null;

        var venue = await _dbContext.Venues.FirstOrDefaultAsync(v => v.Id == menu.VenueId) ??
            throw new ArgumentException($"Venue by ID: {menu.VenueId} not found.");

        _dbContext.Menus.Update(menu);
        await _dbContext.SaveChangesAsync();

        return menu.Id;
    }
}
