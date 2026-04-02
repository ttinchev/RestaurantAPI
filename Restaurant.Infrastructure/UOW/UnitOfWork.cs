using Microsoft.EntityFrameworkCore.Storage;
using Restaurant.Domain.Interfaces;
using Restaurant.Infrastructure.Persistance;

namespace Restaurant.Infrastructure.UOW
{
    public class UnitOfWork(
        RestaurantContext context,
        IOrderRepository orderRepository,
        IMenuItemRepository menuItemRepository,
        ICategoryRepository categoryRepository,
        IPaymentRepository paymentRepository,
        ITableRepository tableRepository) : IUnitOfWork
    {
        private IDbContextTransaction? _transaction;

        public IOrderRepository OrdersRepository { get; } = orderRepository;
        public IMenuItemRepository MenuItemRepository { get; } = menuItemRepository;
        public ICategoryRepository CategoryRepository { get; } = categoryRepository;
        public IPaymentRepository PaymentRepository { get; } = paymentRepository;
        public ITableRepository TableRepository { get; } = tableRepository;

        public async Task BeginTransaction()
        {
            if (_transaction != null)
            {
                return;
            }

            _transaction = await context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            if (_transaction == null)
            {
                return;
            }

            await context.SaveChangesAsync();
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task RollbackTransaction()
        {
            if (_transaction == null)
            {
                return;
            }

            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
