using Restaurant.Domain.Enums;

namespace Restaurant.Api.DTO.Request
{
    public class CreatePaymentDto
    {
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int TableId { get; set; }
    }
}