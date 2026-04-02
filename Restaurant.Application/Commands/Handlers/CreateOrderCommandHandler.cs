using MediatR;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Commands.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateOrderCommandHandler(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var dbOrderItems = new List<OrderItem>();
            foreach (var orderItem in request.OrderItems)
            {
                dbOrderItems.Add(new OrderItem
                {
                    MenuItemId = orderItem.MenuItemId,
                    Quantity = orderItem.Quantity,
                });
            }
            var dbOrder = new Order
            {
                TableId = request.TableId,
                OrderItems = dbOrderItems
            };
            _unitOfWork.OrdersRepository.AddAsync(dbOrder);
            return _unitOfWork.SaveChangesAsync();
        }
    }
}
