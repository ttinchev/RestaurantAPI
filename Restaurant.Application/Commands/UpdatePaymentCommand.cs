using MediatR;
using Restaurant.Domain.Enums;

namespace Restaurant.Application.Commands
{
    public class UpdatePaymentCommand : IRequest<int>
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public bool IsSuccessful { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int TableId { get; set; }
    }
}