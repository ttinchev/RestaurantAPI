using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;
using Restaurant.Infrastructure.Persistance;

namespace Restaurant.Infrastructure.Repository
{
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        public MenuItemRepository(RestaurantContext context) : base(context)
        {
        }
    }
}
