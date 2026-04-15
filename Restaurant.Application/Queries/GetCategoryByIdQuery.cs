using MediatR;
using Restaurant.Application.Models.Category;

namespace Restaurant.Application.Queries
{
    public class GetCategoryByIdQuery(int id) : IRequest<CategoryResponseModel>
    {
        public int Id { get; set; } = id;
    }
}