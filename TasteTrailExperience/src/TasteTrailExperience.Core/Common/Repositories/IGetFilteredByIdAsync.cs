using TasteTrailExperience.Core.Filters;
using TasteTrailExperience.Core.Filters.Models;

namespace TasteTrailExperience.Core.Common.Repositories;

public interface IGetFilteredByIdAsync<TEntity, TId>
{
    Task<List<TEntity>> GetFilteredByIdAsync(FilterParameters<TEntity> parameters, TId referenceId);
}
