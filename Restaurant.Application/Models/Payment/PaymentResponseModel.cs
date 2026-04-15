using Restaurant.Domain.Enums;

namespace Restaurant.Application.Models.Payment
{
    public class PaymentResponseModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public bool IsSuccessful { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int TableId { get; set; }
    }
}