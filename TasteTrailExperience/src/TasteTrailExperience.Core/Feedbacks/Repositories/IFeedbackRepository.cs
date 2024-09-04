using TasteTrailData.Core.Common.Repositories.Base;
using TasteTrailData.Core.Feedbacks.Models;
using TasteTrailExperience.Core.Common.Repositories;

namespace TasteTrailExperience.Core.Feedbacks.Repositories;

public interface IFeedbackRepository : IGetFilteredByIdAsync<Feedback, int>, IGetFilteredAsync<Feedback>,
IGetAsNoTrackingAsync<Feedback?, int>, IGetCountFilteredIdAsync<Feedback, int>, 
IGetCountFilteredAsync<Feedback>, IGetByIdAsync<Feedback?, int>,
ICreateAsync<Feedback, int>, IDeleteByIdAsync<int, int?>, IPutAsync<Feedback, int?>, 
IIncrementLikesAsync<Feedback, int?>, IDecrementLikesAsync<Feedback, int?> 
{
    public Task<decimal> GetAverageRatingAsync(int venueId);
}
