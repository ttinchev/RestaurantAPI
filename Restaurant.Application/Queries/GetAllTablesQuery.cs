using MediatR;
using Restaurant.Application.Models.Table;

namespace Restaurant.Application.Queries
{
    public class GetAllTablesQuery : IRequest<IEnumerable<TableResponseModel>>
    {
    }
}