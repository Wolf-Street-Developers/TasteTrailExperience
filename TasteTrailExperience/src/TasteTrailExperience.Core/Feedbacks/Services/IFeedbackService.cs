using TasteTrailData.Core.Feedbacks.Models;
using TasteTrailData.Core.Users.Models;
using TasteTrailExperience.Core.Feedbacks.Dtos;
using TasteTrailExperience.Core.Filters;
using TasteTrailExperience.Core.Filters.Dtos;

namespace TasteTrailExperience.Core.Feedbacks.Services;

public interface IFeedbackService
{
    Task<FilterResponseDto<FeedbackGetDto>> GetFeedbacksFilteredAsync(FilterParametersDto filterParameters, int venueId);

    Task<FeedbackGetDto?> GetFeedbackByIdAsync(int id);

    Task<int> GetFeedbacksCountAsync();

    Task<int> CreateFeedbackAsync(FeedbackCreateDto feedback, User user);

    Task<int?> DeleteFeedbackByIdAsync(int id, User user);

    Task<int?> PutFeedbackAsync(FeedbackUpdateDto feedback, User user);
}
