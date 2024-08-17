using TasteTrailData.Core.Venues.Models;

namespace TasteTrailExperience.Core.Venues.Dtos;

public class VenueGetByCountDto
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Address { get; set; }

    public string? Description { get; set; }

    public string? ContactNumber { get; set; }

    public required string Email { get; set; }

    public string? LogoUrlPath { get; set; }

    public float AveragePrice { get; set; }

    public float OverallRating { get; set; }

    public required string UserId { get; set; }
}
