using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;
using Restaurant.Infrastructure.Persistance;

namespace Restaurant.Infrastructure.Repository
{
    public class TableRepository : Repository<Table>, ITableRepository
    {
        public TableRepository(RestaurantContext context) : base(context)
        {
        }
    }
}
