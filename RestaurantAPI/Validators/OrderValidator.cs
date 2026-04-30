using FluentValidation;
using Restaurant.Api.DTO.Request;

namespace Restaurant.Api.Validators
{
    public class OrderValidator : AbstractValidator<CreateOrderDto>
    {
        public OrderValidator()
        {
            RuleFor(x => x.TableId)
                .GreaterThan(0).WithMessage("TableId must be greater than 0.");
            RuleFor(x => x.OrderItems)
                .NotEmpty().WithMessage("Order must contain at least one order item.");

            RuleForEach(x => x.OrderItems).SetValidator(new OrderItemValidator());
        }
    }
}
