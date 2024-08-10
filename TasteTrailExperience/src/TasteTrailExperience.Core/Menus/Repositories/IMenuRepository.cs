using TasteTrailData.Core.Common.Repositories.Base;
using TasteTrailData.Core.Menus.Models;

namespace TasteTrailExperience.Core.Menus.Repositories;

public interface IMenuRepository : IGetByCountAsync<Menu>, IGetByIdAsync<Menu?, int>,
ICreateAsync<Menu, int>, IDeleteByIdAsync<int?>, IPutAsync<Menu, int?> 
{
    
}
