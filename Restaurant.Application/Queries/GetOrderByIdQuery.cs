using MediatR;
using Restaurant.Application.Models;

namespace Restaurant.Application.Queries
{
    public class GetOrderByIdQuery(int id) : IRequest<OrderResponseModel>
    {
        public int Id { get; set; } = id;
    }
}
