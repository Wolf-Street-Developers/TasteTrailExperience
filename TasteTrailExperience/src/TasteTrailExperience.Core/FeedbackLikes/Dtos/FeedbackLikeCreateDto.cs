namespace TasteTrailExperience.Core.FeedbackLikes.Dtos;

public class FeedbackLikeCreateDto
{
    public required string UserId { get; set; }
    public required int FeedbackId { get; set; }
}

