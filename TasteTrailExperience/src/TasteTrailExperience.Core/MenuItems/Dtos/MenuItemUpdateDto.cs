namespace TasteTrailExperience.Core.MenuItems.Dtos;

public class MenuItemUpdateDto
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public float Price { get; set; }

    public int PopularityRate { get; set; }

    public int MenuId { get; set; }
}
