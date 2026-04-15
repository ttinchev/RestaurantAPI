using MediatR;
using Restaurant.Application.Models;
using Restaurant.Application.Models.MenuItem;
using Restaurant.Application.Models.Order;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Queries.Handlers
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetOrderByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<OrderResponseModel> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var dbOrder = await _unitOfWork.OrdersRepository.GetOrderWithMenuItemsAsync(request.Id);

            var result = new OrderResponseModel();

            if (dbOrder != null)
            {
                result.Id = dbOrder.Id;
                result.TableId = dbOrder.TableId;
                result.TotalAmount = dbOrder.TotalPrice;
                result.MenuItems = dbOrder.OrderItems.Select(mi => new MenuItemResponseModel
                {
                    Id = mi.MenuItem.Id,
                    Name = mi.MenuItem.Name,
                    Price = mi.MenuItem.Price
                }).ToList();
            }

            return result;
        }
    }
}
