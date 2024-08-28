using Microsoft.EntityFrameworkCore;
using TasteTrailData.Core.Filters.Specifications;
using TasteTrailData.Core.MenuItems.Models;
using TasteTrailData.Infrastructure.Common.Data;
using TasteTrailExperience.Core.MenuItems.Repositories;

namespace TasteTrailExperience.Infrastructure.MenuItems.Repositories;

public class MenuItemEfCoreRepository : IMenuItemRepository
{
    private readonly TasteTrailDbContext _dbContext;

    public MenuItemEfCoreRepository(TasteTrailDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<List<MenuItem>> GetFilteredByIdAsync(FilterParameters<MenuItem> parameters, int menuId)
    {
        IQueryable<MenuItem> query = _dbContext.Set<MenuItem>();

        query = query.Where(mi => mi.MenuId == menuId);

        if (parameters.Specification is not null)
            query = parameters.Specification.Apply(query);


        if (parameters.SearchTerm is not null)
        {
            query = query.Where(mi =>
                (mi.Name != null && mi.Name.Contains(parameters.SearchTerm, StringComparison.CurrentCultureIgnoreCase)) ||
                (mi.Description != null && mi.Description.Contains(parameters.SearchTerm, StringComparison.CurrentCultureIgnoreCase))
            );
        }   
        
        query = query.Skip((parameters.PageNumber - 1) * parameters.PageSize).Take(parameters.PageSize);

        return await query.ToListAsync();
    }

    public async Task<MenuItem?> GetByIdAsync(int id)
    {
        return await _dbContext.MenuItems
            .FirstOrDefaultAsync(mi => mi.Id == id);
    }

    public async Task<int> GetCountBySpecificationIdAsync(IFilterSpecification<MenuItem>? specification, int menuId)
    {
        var query = _dbContext.MenuItems.AsQueryable();
        query = query.Where(mi => mi.MenuId == menuId);

        if (specification != null)
            query = specification.Apply(query);

        return await query.CountAsync();
    }

    public async Task<int> CreateAsync(MenuItem menuItem)
    {
        ArgumentNullException.ThrowIfNull(menuItem);

        var menu = await _dbContext.Menus.FirstOrDefaultAsync(m => m.Id == menuItem.MenuId) ?? 
            throw new ArgumentException($"Menu by ID: {menuItem.MenuId} not found.");

        await _dbContext.MenuItems.AddAsync(menuItem);
        await _dbContext.SaveChangesAsync();

        return menuItem.Id;
    }

    public async Task<int?> DeleteByIdAsync(int id)
    {
        var menuItem = await _dbContext.MenuItems.FindAsync(id);

        if (menuItem is null)
            return null;
        
        _dbContext.MenuItems.Remove(menuItem);
        await _dbContext.SaveChangesAsync();

        return id;
    }

    public async Task<int?> PutAsync(MenuItem menuItem)
    {
        var menuItemToUpdate = await _dbContext.MenuItems
            .FirstOrDefaultAsync(mi => mi.Id == menuItem.Id);

        if (menuItemToUpdate is null)
            return null;

        
        menuItemToUpdate.Name = menuItem.Name;
        menuItemToUpdate.Description = menuItem.Description;
        menuItemToUpdate.Price = menuItem.Price;

        await _dbContext.SaveChangesAsync();

        return menuItem.Id;
    }

    public async Task<MenuItem?> GetAsNoTrackingAsync(int id)
    {
        return await _dbContext.MenuItems
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == id);
    }
}
