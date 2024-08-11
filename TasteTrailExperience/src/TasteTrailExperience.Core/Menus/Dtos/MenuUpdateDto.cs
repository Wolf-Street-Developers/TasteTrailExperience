namespace TasteTrailExperience.Core.Menus.Dtos;

public class MenuUpdateDto
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public required int VenueId { get; set; }
}
