using TasteTrailData.Core.Menus.Models;
using TasteTrailData.Core.Users.Models;
using TasteTrailData.Infrastructure.Filters.Dtos;
using TasteTrailExperience.Core.Filters;
using TasteTrailExperience.Core.Menus.Dtos;

namespace TasteTrailExperience.Core.Menus.Services;

public interface IMenuService
{
    Task<FilterResponseDto<Menu>> GetMenusFilteredAsync(PaginationParametersDto filterParameters, int venueId);

    Task<FilterResponseDto<Menu>> GetMenusFilteredAsync(PaginationSearchParametersDto filterParameters);

    Task<Menu?> GetMenuByIdAsync(int id);

    Task<int> CreateMenuAsync(MenuCreateDto menu, User user);

    Task<int?> DeleteMenuByIdAsync(int id, User user);
    
    Task<int?> PutMenuAsync(MenuUpdateDto menu, User user);
}
