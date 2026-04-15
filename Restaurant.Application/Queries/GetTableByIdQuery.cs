using MediatR;
using Restaurant.Application.Models.Table;

namespace Restaurant.Application.Queries
{
    public class GetTableByIdQuery(int id) : IRequest<TableResponseModel>
    {
        public int Id { get; set; } = id;
    }
}