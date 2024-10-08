using FluentValidation;
using TasteTrailExperience.Core.FeedbackLikes.Dtos;

namespace TasteTrailExperience.Api.FeedbackLikes.Validators;

public class FeedbackLikeCreateDtoValidator : AbstractValidator<FeedbackLikeCreateDto>
{
    public FeedbackLikeCreateDtoValidator()
    {
        RuleFor(f => f.FeedbackId)
            .NotEmpty()
            .GreaterThan(0);
    }
}
