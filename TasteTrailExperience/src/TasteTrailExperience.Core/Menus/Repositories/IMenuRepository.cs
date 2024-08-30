using TasteTrailData.Core.Common.Repositories.Base;
using TasteTrailData.Core.Menus.Models;
using TasteTrailExperience.Core.Common.Repositories;

namespace TasteTrailExperience.Core.Menus.Repositories;

public interface IMenuRepository : IGetFilteredByIdAsync<Menu, int>, IGetCountFilteredIdAsync<Menu, int>, 
    IGetAsNoTrackingAsync<Menu?, int>, IGetByIdAsync<Menu?, int>,
    ICreateAsync<Menu, int>, IDeleteByIdAsync<int, int?>, IPutAsync<Menu, int?> 
{
}
