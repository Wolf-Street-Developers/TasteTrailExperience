using FluentValidation;
using TasteTrailExperience.Core.MenuItemLikes.Dtos;

namespace TasteTrailExperience.Api.MenuItemLikes.Validators;

public class MenuItemLikeCreateDtoValidator : AbstractValidator<MenuItemLikeCreateDto>
{
    public MenuItemLikeCreateDtoValidator()
    {
        RuleFor(f => f.MenuItemId)
            .NotEmpty()
            .GreaterThan(0);
    }
}
