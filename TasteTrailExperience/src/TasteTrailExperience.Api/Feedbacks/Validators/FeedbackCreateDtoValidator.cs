using FluentValidation;
using TasteTrailExperience.Core.Feedbacks.Dtos;

namespace TasteTrailExperience.Api.Feedbacks.Validators;

public class FeedbackCreateDtoValidator : AbstractValidator<FeedbackCreateDto>
{
    public FeedbackCreateDtoValidator()
    {
        RuleFor(f => f.VenueId)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Invalid Venue ID.");

        RuleFor(f => f.Rating)
            .GreaterThan(-1)
            .LessThan(6)
            .WithMessage("Invalid rating.");
    }
}