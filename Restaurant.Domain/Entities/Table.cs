namespace Restaurant.Domain.Entities
{
    public class Table
    {
        public int Id { get; set; }
        public int NumberOfSeats { get; set; }
        public decimal TotalBill { get { return TableOrders.Sum(o => o.TotalPrice); } }
        public bool IsFree { get; set; }
        public bool Enabled { get; set; }

        public ICollection<Order> TableOrders { get; set; } = [];
        public ICollection<Payment> Payments { get; set; } = [];
    }
}
