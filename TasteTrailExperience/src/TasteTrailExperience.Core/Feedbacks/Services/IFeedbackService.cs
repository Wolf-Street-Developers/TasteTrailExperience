using TasteTrailData.Core.Feedbacks.Models;
using TasteTrailData.Core.Users.Models;
using TasteTrailExperience.Core.Feedbacks.Dtos;

namespace TasteTrailExperience.Core.Feedbacks.Services;

public interface IFeedbackService
{
    Task<List<FeedbackGetDto>> GetFeedbacksByCountAsync(int count);

    Task<FeedbackGetDto?> GetFeedbackByIdAsync(int id);

    Task<int> CreateFeedbackAsync(FeedbackCreateDto feedback, User user);

    Task<int?> DeleteFeedbackByIdAsync(int id);

    Task<int?> UpdateFeedbackAsync(FeedbackUpdateDto feedback, User user);
}
