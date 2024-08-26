using TasteTrailExperience.Core.Filters;
using TasteTrailExperience.Core.Filters.Models;

namespace TasteTrailExperience.Core.Common.Repositories;

public interface IGetFilteredByIdAsync<TEntity, TId>
{
    Task<IEnumerable<TEntity>> GetFilteredByIdAsync(FilterParameters<TEntity> parameters, TId referenceId);
}
