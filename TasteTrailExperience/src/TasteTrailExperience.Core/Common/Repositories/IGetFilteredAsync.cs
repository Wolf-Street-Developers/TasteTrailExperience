using TasteTrailExperience.Core.Filters;

namespace TasteTrailExperience.Core.Common.Repositories;

public interface IGetFilteredAsync<TEntity>
{
    Task<IEnumerable<TEntity>> GetFilteredAsync(IFilterSpecification<TEntity> specification, int pageNumber, int pageSize);
}
