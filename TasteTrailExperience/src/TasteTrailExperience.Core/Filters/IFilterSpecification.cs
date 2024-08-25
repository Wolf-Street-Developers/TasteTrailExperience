namespace TasteTrailExperience.Core.Filters;

public interface IFilterSpecification<T>
{
    IQueryable<T> Apply(IQueryable<T> query);
}
