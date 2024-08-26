using TasteTrailData.Core.Feedbacks.Models;

namespace TasteTrailExperience.Core.Filters.Base;

public interface IFeedbackable
{
    ICollection<Feedback> Feedbacks { get; }
}
