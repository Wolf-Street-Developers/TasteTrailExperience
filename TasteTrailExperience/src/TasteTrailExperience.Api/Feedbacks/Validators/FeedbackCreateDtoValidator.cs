using FluentValidation;
using TasteTrailExperience.Core.Feedbacks.Dtos;

namespace TasteTrailExperience.Api.Feedbacks.Validators;

public class FeedbackCreateDtoValidator : AbstractValidator<FeedbackCreateDto>
{
    public FeedbackCreateDtoValidator()
    {
        RuleFor(f => f.VenueId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(f => f.Rating)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(5);
    }
}