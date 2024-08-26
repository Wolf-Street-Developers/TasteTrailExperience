using TasteTrailExperience.Core.Specifications.Filters;

namespace TasteTrailExperience.Core.Common.Repositories;

public interface IGetCountBySpecificationIdAsync<TEntity, TId>
{
    Task<int> GetCountBySpecificationIdAsync(IFilterSpecification<TEntity>? specification, TId referenceId);
}
