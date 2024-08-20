using FluentValidation;
using TasteTrailExperience.Core.Menus.Dtos;

namespace TasteTrailExperience.Api.Menus.Validators;

public class MenuCreateDtoValidator : AbstractValidator<MenuCreateDto>
{
    public MenuCreateDtoValidator()
    {
        RuleFor(m => m.Name)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(m => m.Description)
            .MaximumLength(500);

        RuleFor(m => m.VenueId)
            .NotEmpty()
            .GreaterThan(0);
    }
}