using FluentValidation;
using Restaurant.Api.DTO.Request;

namespace Restaurant.Api.Validators
{
    public class UpdatePaymentValidator : AbstractValidator<UpdatePaymentDto>
    {
        public UpdatePaymentValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Payment amount must be greater than 0.");

            RuleFor(x => x.PaymentMethod)
                .IsInEnum().WithMessage("Invalid payment method.");

            RuleFor(x => x.TableId)
                .GreaterThan(0).WithMessage("TableId must be greater than 0.");
        }
    }
}