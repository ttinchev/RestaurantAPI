namespace Restaurant.Application.Models
{
    public class OrderResponseModel
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<MenuItemResponseModel> MenuItems { get; set; } = new List<MenuItemResponseModel>();
    }
}
