using FluentValidation;
using Restaurant.Api.DTO.Request;

namespace Restaurant.Api.Validators
{
    public class UpdateTableValidator : AbstractValidator<UpdateTableDto>
    {
        public UpdateTableValidator()
        {
            RuleFor(x => x.NumberOfSeats)
                .GreaterThan(0).WithMessage("Number of seats must be greater than 0.")
                .LessThanOrEqualTo(20).WithMessage("Number of seats must not exceed 20.");
        }
    }
}