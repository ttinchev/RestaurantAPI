using Restaurant.Application.Models.Order;

namespace Restaurant.Api.DTO.Request
{
    public class CreateOrderDto
    {
        public int TableId { get; set; }
        public List<OrderItemRequestModel> OrderItems { get; set; } = [];
    }
}
