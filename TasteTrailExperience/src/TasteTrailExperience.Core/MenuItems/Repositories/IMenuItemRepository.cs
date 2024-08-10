using TasteTrailData.Core.Common.Repositories.Base;
using TasteTrailData.Core.MenuItems.Models;

namespace TasteTrailExperience.Core.MenuItems.Repositories;

public interface IMenuItemRepository : IGetByCountAsync<MenuItem>, IGetByIdAsync<MenuItem?, int>,
ICreateAsync<MenuItem, int>, IDeleteByIdAsync<int?>, IPutAsync<MenuItem, int?> 
{
    
}
