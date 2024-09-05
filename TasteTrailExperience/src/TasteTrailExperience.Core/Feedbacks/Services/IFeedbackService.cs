using TasteTrailData.Core.Users.Models;
using TasteTrailData.Infrastructure.Filters.Dtos;
using TasteTrailExperience.Core.Feedbacks.Dtos;

namespace TasteTrailExperience.Core.Feedbacks.Services;

public interface IFeedbackService
{
    Task<FilterResponseDto<FeedbackGetDto>> GetFeedbacksFilteredAsync(FilterParametersDto filterParameters, int venueId, User? authenticatedUser);

    Task<FilterResponseDto<FeedbackGetDto>> GetFeedbacksFilteredAsync(FilterParametersSearchDto filterParameters, User? authenticatedUser);

    Task<FeedbackGetDto?> GetFeedbackByIdAsync(int id);

    Task<int> GetFeedbacksCountAsync();

    Task<int> CreateFeedbackAsync(FeedbackCreateDto feedback, User user);

    Task<int?> DeleteFeedbackByIdAsync(int id, User user);

    Task<int?> PutFeedbackAsync(FeedbackUpdateDto feedback, User user);
}
