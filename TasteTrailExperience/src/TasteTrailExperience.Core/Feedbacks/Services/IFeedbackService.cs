using TasteTrailData.Core.Feedbacks.Models;
using TasteTrailData.Core.Users.Models;
using TasteTrailExperience.Core.Feedbacks.Dtos;
using TasteTrailExperience.Core.Filters;

namespace TasteTrailExperience.Core.Feedbacks.Services;

public interface IFeedbackService
{
    Task<List<FeedbackGetDto>> GetFeedbacksFiltered(int venueId, IFilterSpecification<Feedback> specification, 
        int pageNumber, int pageSize);

    Task<FeedbackGetDto?> GetFeedbackByIdAsync(int id);

    Task<int> GetFeedbacksCountAsync();

    Task<int> CreateFeedbackAsync(FeedbackCreateDto feedback, User user);

    Task<int?> DeleteFeedbackByIdAsync(int id, User user);

    Task<int?> PutFeedbackAsync(FeedbackUpdateDto feedback, User user);
}
