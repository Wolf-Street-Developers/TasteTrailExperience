using TasteTrailExperience.Core.Filters;
using TasteTrailExperience.Core.Filters.Models;

namespace TasteTrailExperience.Core.Common.Repositories;

public interface IGetFilteredAsync<TEntity>
{
    Task<IEnumerable<TEntity>> GetFilteredAsync(FilterParameters<TEntity> parameters);
}
