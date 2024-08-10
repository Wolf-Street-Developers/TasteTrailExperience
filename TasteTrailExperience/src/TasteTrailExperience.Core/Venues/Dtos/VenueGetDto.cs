using TasteTrailData.Core.Feedbacks.Models;
using TasteTrailData.Core.Menus.Models;

namespace TasteTrailExperience.Core.Venues.Dtos;

public class VenueGetDto
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public required string Address { get; set; }

    public string? Description { get; set; }
    
    public required string Email { get; set; }

    public string? ContactNumber { get; set; }

    public float AveragePrice { get; set; }

    public float OverallRating { get; set; }

    public required ICollection<Menu>? Menus { get; set; }

    public required ICollection<Feedback>? Feedbacks { get; set; }
}
