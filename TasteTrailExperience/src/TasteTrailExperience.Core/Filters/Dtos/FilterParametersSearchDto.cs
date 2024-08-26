using TasteTrailExperience.Core.Filters.Enums;

namespace TasteTrailExperience.Core.Filters.Dtos;

public class FilterParametersSearchDto
{
    public required FilterType? Type { get; set; }
    
    public int PageNumber { get; set; }
    
    public int PageSize { get; set; }

    public string? SearchTerm { get; set; }
}
