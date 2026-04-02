namespace Restaurant.Domain.Entities
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal UnitPrice { get { return MenuItem.Price; } }
    }
}
