using TasteTrailData.Core.Common.Repositories.Base;
using TasteTrailData.Core.Menus.Models;

namespace TasteTrailExperience.Core.Menus.Repositories;

public interface IMenuRepository : IGetFromToFilterAsync<Menu>, IGetAsNoTrackingAsync<Menu?, int>, IGetByIdAsync<Menu?, int>,
ICreateAsync<Menu, int>, IDeleteByIdAsync<int, int?>, IPutAsync<Menu, int?> 
{
}
