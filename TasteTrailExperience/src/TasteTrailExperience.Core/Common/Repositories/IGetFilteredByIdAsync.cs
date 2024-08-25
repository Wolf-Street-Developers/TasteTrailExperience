using TasteTrailExperience.Core.Filters;

namespace TasteTrailExperience.Core.Common.Repositories;

public interface IGetFilteredByIdAsync<TEntity, TId>
{
    Task<IEnumerable<TEntity>> GetFilteredByIdAsync(TId referenceId, IFilterSpecification<TEntity> specification, int pageNumber, int pageSize);
}
