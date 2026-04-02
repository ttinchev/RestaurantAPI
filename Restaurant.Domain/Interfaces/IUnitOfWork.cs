namespace Restaurant.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IOrderRepository OrdersRepository { get; }
        IMenuItemRepository MenuItemRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        ITableRepository TableRepository { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();
    }
}
