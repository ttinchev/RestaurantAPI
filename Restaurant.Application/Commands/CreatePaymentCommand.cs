using MediatR;
using Restaurant.Domain.Enums;

namespace Restaurant.Application.Commands
{
    public class CreatePaymentCommand : IRequest<int>
    {
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int TableId { get; set; }
    }
}