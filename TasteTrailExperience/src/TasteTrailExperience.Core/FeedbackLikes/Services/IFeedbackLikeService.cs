using TasteTrailData.Core.Users.Models;
using TasteTrailExperience.Core.FeedbackLikes.Dtos;

namespace TasteTrailExperience.Core.FeedbackLikes.Services;

public interface IFeedbackLikeService
{
    Task<int> CreateFeedbackLikeAsync(FeedbackLikeCreateDto menuItem, User user);

    Task<int?> DeleteFeedbackLikeByIdAsync(int id, User user);
}
