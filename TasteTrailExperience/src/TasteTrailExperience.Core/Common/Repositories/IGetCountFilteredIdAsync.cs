using TasteTrailData.Core.Filters.Specifications;

namespace TasteTrailExperience.Core.Common.Repositories;

public interface IGetCountFilteredIdAsync<TEntity, TId>
{
    Task<int> GetCountFilteredIdAsync(FilterParameters<TEntity>? parameters, TId referenceId);
}
