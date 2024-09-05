using TasteTrailData.Core.Common.Repositories.Base;
using TasteTrailData.Core.FeedbackLikes.Models;

namespace TasteTrailExperience.Core.FeedbackLikes.Repositories;

public interface IFeedbackLikeRepository : ICreateAsync<FeedbackLike, int>, IDeleteByIdAsync<int, int?>, IGetAsNoTrackingAsync<FeedbackLike, int>
{
    Task<List<int>> GetLikedFeedbacksIds(string userId);

    Task<bool> Exists(int feedbackId, string userId);
}