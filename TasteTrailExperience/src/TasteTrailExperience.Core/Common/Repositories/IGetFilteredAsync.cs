using TasteTrailExperience.Core.Filters.Models;

namespace TasteTrailExperience.Core.Common.Repositories;

public interface IGetFilteredAsync<TEntity>
{
    Task<List<TEntity>> GetFilteredAsync(FilterParameters<TEntity> parameters);
}
