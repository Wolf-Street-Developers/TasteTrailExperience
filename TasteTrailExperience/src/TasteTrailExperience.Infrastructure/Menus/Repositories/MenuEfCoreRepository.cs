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

    public async Task<List<Menu>> GetFromToFilterAsync(int from, int to, Predicate<Menu> filter)
    {
        return await _dbContext.Menus
            .Where(m => filter(m))
            .Skip(from - 1)
            .Take(to - from + 1)
            .ToListAsync();
    }

    public async Task<Menu?> GetByIdAsync(int id)
    {
        return await _dbContext.Menus
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<int> CreateAsync(Menu menu)
    {
        ArgumentNullException.ThrowIfNull(menu);

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
        var menuToUpdate = await _dbContext.Menus
            .FirstOrDefaultAsync(m => m.Id == menu.Id);

        if (menuToUpdate is null)
            return null;

        menuToUpdate.Name = menu.Name;
        menuToUpdate.Description = menu.Description;

        await _dbContext.SaveChangesAsync();

        return menu.Id;
    }

    public async Task<Menu?> GetAsNoTrackingAsync(int id) 
    {
        return await _dbContext.Menus
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == id);
    }
}
