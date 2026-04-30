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
        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if(request.OrderItems == null || !request.OrderItems.Any())
            {
                throw new ArgumentException("Order must contain at least one item.");
            }

            if(request.TableId <= 0)
            {
                throw new ArgumentException("Invalid table ID.");
            }

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

            await _unitOfWork.OrdersRepository.AddAsync(dbOrder);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
