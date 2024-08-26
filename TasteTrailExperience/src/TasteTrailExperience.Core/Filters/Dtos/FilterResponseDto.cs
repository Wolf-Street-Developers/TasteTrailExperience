namespace TasteTrailExperience.Core.Filters.Dtos;

public class FilterResponseDto<TEntity>
{
    public int CurrentPage { get; set; }

    public int AmountOfPages { get; set; }

    public int AmountOfEntities { get; set; }

    public required List<TEntity> Entities { get; set; }
}
