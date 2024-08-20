using FluentValidation;
using TasteTrailExperience.Core.Menus.Dtos;

namespace TasteTrailExperience.Api.Menus.Validators;

public class MenuUpdateDtoValidator : AbstractValidator<MenuUpdateDto>
{
    public MenuUpdateDtoValidator()
    {
        RuleFor(m => m.Id)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(m => m.Name)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(mi => mi.Description)
            .MaximumLength(500);
    }
}