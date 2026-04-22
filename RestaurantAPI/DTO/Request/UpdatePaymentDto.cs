using Restaurant.Domain.Enums;

namespace Restaurant.Api.DTO.Request
{
    public class UpdatePaymentDto
    {
        public decimal Amount { get; set; }
        public bool IsSuccessful { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int TableId { get; set; }
    }
}