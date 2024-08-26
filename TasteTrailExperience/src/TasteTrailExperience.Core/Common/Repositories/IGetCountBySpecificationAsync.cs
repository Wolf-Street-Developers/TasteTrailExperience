using TasteTrailExperience.Core.Specifications.Filters;

namespace TasteTrailExperience.Core.Common.Repositories;

public interface IGetCountBySpecificationAsync<TEntity, TId>
{
    Task<int> GetCountBySpecificationAsync(IFilterSpecification<TEntity>? specification, TId referenceId);
}
