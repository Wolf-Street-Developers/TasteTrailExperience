using TasteTrailData.Core.Common.Repositories.Base;
using TasteTrailData.Core.MenuItems.Models;
using TasteTrailExperience.Core.Common.Repositories;

namespace TasteTrailExperience.Core.MenuItems.Repositories;

public interface IMenuItemRepository : IGetFilteredByIdAsync<MenuItem, int>, IGetAsNoTrackingAsync<MenuItem?, int>, 
IGetCountFilteredIdAsync<MenuItem, int>, IGetByIdAsync<MenuItem?, int>,
ICreateAsync<MenuItem, int>, IDeleteByIdAsync<int, int?>, IPutAsync<MenuItem, int?>,
IIncrementLikesAsync<MenuItem, int?>, IDecrementLikesAsync<MenuItem, int?> 
{
}
