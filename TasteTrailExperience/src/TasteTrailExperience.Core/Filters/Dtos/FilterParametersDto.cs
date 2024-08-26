using TasteTrailExperience.Core.Filters.Enums;

namespace TasteTrailExperience.Core.Filters.Dtos;

public class FilterParametersDto
{
    public required FilterType? Type { get; set; }
    
    public int PageNumber { get; set; }
    
    public int PageSize { get; set; }
}
