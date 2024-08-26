using TasteTrailData.Core.Common.Repositories.Base;
using TasteTrailData.Core.MenuItems.Models;

namespace TasteTrailExperience.Core.MenuItems.Repositories;

public interface IMenuItemRepository : IGetFilteredByIdAsync<MenuItem, int>, IGetAsNoTrackingAsync<MenuItem?, int>, 
IGetCountBySpecificationIdAsync<MenuItem, int>, IGetByIdAsync<MenuItem?, int>,
ICreateAsync<MenuItem, int>, IDeleteByIdAsync<int, int?>, IPutAsync<MenuItem, int?> 
{
}
