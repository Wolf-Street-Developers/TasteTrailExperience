
using TasteTrailData.Core.Common.Repositories.Base;
using TasteTrailData.Core.Feedbacks.Models;

namespace TasteTrailExperience.Core.Feedbacks.Repositories;

public interface IFeedbackRepository : IGetByCountAsync<Feedback>, IGetByIdAsync<Feedback?, int>,
ICreateAsync<Feedback, int>, IDeleteByIdAsync<int?>, IPutAsync<Feedback, int?>
{
    
}
