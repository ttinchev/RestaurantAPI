using MediatR;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Commands.Handlers
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.OrdersRepository.GetOrderWithMenuItemsAsync(request.Id);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            order.TableId = request.TableId;
            order.OrderItems = request.OrderItems.Select(oi => new Domain.Entities.OrderItem
            {
                MenuItemId = oi.MenuItemId,
                Quantity = oi.Quantity
            }).ToList();
            _unitOfWork.OrdersRepository.Update(order);
            await _unitOfWork.SaveChangesAsync();
            return order.Id;
        }
    }
}
