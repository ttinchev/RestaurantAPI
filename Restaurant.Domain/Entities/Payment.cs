using Restaurant.Domain.Enums;

namespace Restaurant.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public bool IsSuccessful { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public int TableId { get; set; }
        public Table Table { get; set; } = null!;
    }
}
