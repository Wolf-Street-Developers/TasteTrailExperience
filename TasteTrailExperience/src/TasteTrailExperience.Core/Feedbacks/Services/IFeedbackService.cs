using TasteTrailData.Core.Feedbacks.Models;

namespace TasteTrailExperience.Core.Feedbacks.Services;

public interface IFeedbackService
{
    Task<IEnumerable<Feedback>> GetFeedbacksByCountAsync(int count);

    Task<Feedback> GetFeedbackByIdAsync(int count);

    Task<int> CreateFeedbackAsync(Feedback feedback);

    Task<int> DeleteFeedbackByIdAsync(int id);

    Task<int> UpdateFeedbackAsync(Feedback feedback);
}
