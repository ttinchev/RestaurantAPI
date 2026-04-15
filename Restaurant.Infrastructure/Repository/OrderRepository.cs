using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;
using Restaurant.Infrastructure.Persistance;

namespace Restaurant.Infrastructure.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(RestaurantContext context) : base(context)
        {
        }

        public async Task<Order?> GetOrderWithMenuItemsAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllWithMenuItemsAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
