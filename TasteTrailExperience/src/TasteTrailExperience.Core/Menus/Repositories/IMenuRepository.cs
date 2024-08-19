using TasteTrailData.Core.Common.Repositories.Base;
using TasteTrailData.Core.Menus.Models;

namespace TasteTrailExperience.Core.Menus.Repositories;

public interface IMenuRepository : IGetFromToAsync<Menu>, IGetByIdAsync<Menu?, int>,
ICreateAsync<Menu, int>, IDeleteByIdAsync<int, int?>, IPutAsync<Menu, int?> 
{
    Task<Menu?> GetAsNoTrackingAsync(int id);
}
