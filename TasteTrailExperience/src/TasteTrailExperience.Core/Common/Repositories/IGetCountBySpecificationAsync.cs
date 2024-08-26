using TasteTrailExperience.Core.Specifications.Filters;

namespace TasteTrailExperience.Core.Common.Repositories;

public interface IGetCountBySpecificationAsync<TEntity>
{
    Task<int> GetCountBySpecificationAsync(IFilterSpecification<TEntity>? specification);
}
