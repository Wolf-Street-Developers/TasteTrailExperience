using TasteTrailData.Core.Common.Repositories.Base;
using TasteTrailData.Core.Feedbacks.Models;

namespace TasteTrailExperience.Core.Feedbacks.Repositories;

public interface IFeedbackRepository : IGetFilteredByIdAsync<Feedback, int>, IGetAsNoTrackingAsync<Feedback?, int>, IGetCountAsync, 
IGetCountBySpecificationIdAsync<Feedback, int>, IGetByIdAsync<Feedback?, int>,
ICreateAsync<Feedback, int>, IDeleteByIdAsync<int, int?>, IPutAsync<Feedback, int?>, 
IIncrementLikesAsync<Feedback, int?>, IDecrementLikesAsync<Feedback, int?> 
{
    public Task<decimal> GetAverageRatingAsync(int venueId);
}
