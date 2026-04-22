using Restaurant.Application.Models.Order;

namespace Restaurant.Api.DTO.Request
{
    public class UpdateOrderDto
    {
        public int TableId { get; set; }
        public List<OrderItemRequestModel> OrderItems { get; set; } = [];
    }
}