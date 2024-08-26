using FluentValidation;
using TasteTrailExperience.Core.Filters.Dtos;

namespace TasteTrailExperience.Api.Filters.Validators;

public class FilterParametersDtoValidator : AbstractValidator<FilterParametersDto>
{
    public FilterParametersDtoValidator()
    {
        RuleFor(fp => fp.PageNumber)
            .NotEmpty()
            .GreaterThan(0);
        
        RuleFor(fp => fp.PageSize)
            .NotEmpty()
            .GreaterThan(0)
            .LessThan(100);
    }
}
