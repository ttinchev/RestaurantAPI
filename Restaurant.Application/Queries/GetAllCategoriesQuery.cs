using MediatR;
using Restaurant.Application.Models.Category;

namespace Restaurant.Application.Queries
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryResponseModel>>
    {
    }
}