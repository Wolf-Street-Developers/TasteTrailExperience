using TasteTrailData.Core.Menus.Models;

namespace TasteTrailExperience.Core.Menus.Dtos;

public class MenuGetByCountDto
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public int VenueId { get; set; }
}
