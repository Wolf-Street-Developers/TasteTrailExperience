using TasteTrailData.Core.Common.Repositories.Base;
using TasteTrailData.Core.MenuItems.Models;

namespace TasteTrailExperience.Core.MenuItems.Repositories;

public interface IMenuItemRepository : IGetFilteredByIdAsync<MenuItem, int>, IGetFilteredAsync<MenuItem>,
IGetAsNoTrackingAsync<MenuItem?, int>, IGetCountFilteredIdAsync<MenuItem, int>, IGetCountFilteredAsync<MenuItem>,
IGetByIdAsync<MenuItem?, int>, ICreateAsync<MenuItem, int>, IDeleteByIdAsync<int, int?>, IPutAsync<MenuItem, int?>,
IIncrementLikesAsync<MenuItem, int?>, IDecrementLikesAsync<MenuItem, int?> 
{
}
