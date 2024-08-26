using FluentValidation;
using TasteTrailExperience.Core.Filters.Dtos;

namespace TasteTrailExperience.Api.Filters.Validators;

public class FilterParametersSearchDtoValidator : AbstractValidator<FilterParametersSearchDto>
{
    public FilterParametersSearchDtoValidator()
    {
        RuleFor(fp => fp.PageNumber)
            .NotEmpty()
            .GreaterThan(0);
        
        RuleFor(fp => fp.PageSize)
            .NotEmpty()
            .GreaterThan(0)
            .LessThan(100);

        RuleFor(fp => fp.SearchTerm)
            .MaximumLength(500);
    }
}
