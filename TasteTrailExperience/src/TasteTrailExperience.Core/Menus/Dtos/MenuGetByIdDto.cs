using TasteTrailData.Core.MenuItems.Models;

namespace TasteTrailExperience.Core.Menus.Dtos;

public class MenuGetByIdDto
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public int VenueId { get; set; }

    public required ICollection<MenuItem> MenuItems { get; set; }
}
