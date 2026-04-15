using MediatR;
using Restaurant.Application.Models.Order;

namespace Restaurant.Application.Queries
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<OrderResponseModel>>
    {
    }
}