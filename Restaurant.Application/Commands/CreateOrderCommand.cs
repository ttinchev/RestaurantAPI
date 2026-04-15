using MediatR;
using Restaurant.Application.Models.Order;

namespace Restaurant.Application.Commands
{
    public class CreateOrderCommand : IRequest<int>
    {
        public int TableId { get; set; }
        public List<OrderItemRequestModel> OrderItems { get; set; } = new List<OrderItemRequestModel>();
    }
}
