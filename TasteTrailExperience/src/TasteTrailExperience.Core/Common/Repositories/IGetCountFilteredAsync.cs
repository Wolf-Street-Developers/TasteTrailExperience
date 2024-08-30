using TasteTrailData.Core.Filters.Specifications;

namespace TasteTrailExperience.Core.Common.Repositories;

public interface IGetCountFilteredAsync<TEntity>
{
    Task<int> GetCountFilteredAsync(FilterParameters<TEntity>? parameters);
}
