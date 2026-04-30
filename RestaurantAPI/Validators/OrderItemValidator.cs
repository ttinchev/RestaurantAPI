using FluentValidation;
using Restaurant.Application.Models.Order;

namespace Restaurant.Api.Validators
{
    public class OrderItemValidator : AbstractValidator<OrderItemRequestModel>
    {
        public OrderItemValidator()
        {
            RuleFor(x => x.MenuItemId)
                .GreaterThan(0).WithMessage("MenuItemId must be greater than 0.");
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        }
    }
}
