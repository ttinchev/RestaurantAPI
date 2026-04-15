using MediatR;
using Restaurant.Application.Models.MenuItem;

namespace Restaurant.Application.Queries
{
    public class GetAllMenuItemsQuery : IRequest<IEnumerable<MenuItemResponseModel>>
    {
    }
}