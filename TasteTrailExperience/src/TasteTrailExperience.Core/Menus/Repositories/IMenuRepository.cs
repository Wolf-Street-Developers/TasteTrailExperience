using TasteTrailData.Core.Common.Repositories.Base;
using TasteTrailData.Core.Menus.Models;
using TasteTrailExperience.Core.Common.Repositories;

namespace TasteTrailExperience.Core.Menus.Repositories;

public interface IMenuRepository : IGetFilteredByIdAsync<Menu, int>, IGetFilteredAsync<Menu>, IGetCountFilteredIdAsync<Menu, int>, 
    IGetCountFilteredAsync<Menu>, IGetAsNoTrackingAsync<Menu?, int>, IGetByIdAsync<Menu?, int>,
    ICreateAsync<Menu, int>, IDeleteByIdAsync<int, int?>, IPutAsync<Menu, int?> 
{
}
