using TasteTrailData.Core.Common.Repositories.Base;
using TasteTrailData.Core.Feedbacks.Models;
using TasteTrailExperience.Core.Common.Repositories;

namespace TasteTrailExperience.Core.Feedbacks.Repositories;

public interface IFeedbackRepository : IGetFilteredByIdAsync<Feedback, int>, IGetAsNoTrackingAsync<Feedback?, int>, IGetCountAsync, IGetByIdAsync<Feedback?, int>,
ICreateAsync<Feedback, int>, IDeleteByIdAsync<int, int?>, IPutAsync<Feedback, int?>
{
}
