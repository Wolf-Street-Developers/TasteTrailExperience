namespace TasteTrailExperience.Core.Menus.Dtos;

public class MenuGetCountDto
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public int VenueId { get; set; }
}
