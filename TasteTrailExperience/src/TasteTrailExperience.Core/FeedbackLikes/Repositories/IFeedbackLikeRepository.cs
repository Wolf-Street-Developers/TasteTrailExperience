using TasteTrailData.Core.Common.Repositories.Base;
using TasteTrailData.Core.FeedbackLikes.Models;

namespace TasteTrailExperience.Core.FeedbackLikes.Repositories;

public interface IFeedbackLikeRepository : ICreateAsync<FeedbackLike, int>, IDeleteByIdAsync<int, int?>
{
}