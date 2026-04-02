using System.Xml.Schema;

namespace Restaurant.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public decimal TotalPrice { get { return OrderItems.Sum(x => x.Quantity * x.UnitPrice); } }

        public int TableId { get; set; }
        public Table Table { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }
}
