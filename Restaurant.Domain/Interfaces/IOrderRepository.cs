using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order?> GetOrderWithMenuItemsAsync(int id);
        Task<IEnumerable<Order>> GetAllWithMenuItemsAsync();
    }
}
