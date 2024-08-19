using TasteTrailData.Core.Common.Repositories.Base;
using TasteTrailData.Core.MenuItems.Models;

namespace TasteTrailExperience.Core.MenuItems.Repositories;

public interface IMenuItemRepository : IGetFromToAsync<MenuItem>, IGetByIdAsync<MenuItem?, int>,
ICreateAsync<MenuItem, int>, IDeleteByIdAsync<int, int?>, IPutAsync<MenuItem, int?> 
{
    Task<MenuItem?> GetAsNoTrackingAsync(int id);
}
