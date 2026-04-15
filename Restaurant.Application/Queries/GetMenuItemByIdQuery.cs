using MediatR;
using Restaurant.Application.Models.MenuItem;

namespace Restaurant.Application.Queries
{
    public class GetMenuItemByIdQuery(int id) : IRequest<MenuItemResponseModel>
    {
        public int Id { get; set; } = id;
    }
}