using FluentValidation;
using Restaurant.Api.DTO.Request;

namespace Restaurant.Api.Validators
{
    public class CreateMenuItemValidator : AbstractValidator<CreateMenuItemDto>
    {
        public CreateMenuItemValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Menu item name is required.")
                .MaximumLength(200).WithMessage("Menu item name must not exceed 200 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId must be greater than 0.");
        }
    }
}