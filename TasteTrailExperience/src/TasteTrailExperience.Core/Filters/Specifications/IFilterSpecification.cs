namespace TasteTrailExperience.Core.Specifications.Filters;

public interface IFilterSpecification<T>
{
    IQueryable<T> Apply(IQueryable<T> query);
}
