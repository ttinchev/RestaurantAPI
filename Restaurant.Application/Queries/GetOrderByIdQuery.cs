using MediatR;
using Restaurant.Application.Models.Order;

namespace Restaurant.Application.Queries
{
    public class GetOrderByIdQuery(int id) : IRequest<OrderResponseModel>
    {
        public int Id { get; set; } = id;
    }
}
