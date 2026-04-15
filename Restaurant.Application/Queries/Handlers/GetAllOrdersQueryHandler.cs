using MediatR;
using Restaurant.Application.Models.MenuItem;
using Restaurant.Application.Models.Order;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Queries.Handlers
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderResponseModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllOrdersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OrderResponseModel>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var dbOrders = await _unitOfWork.OrdersRepository.GetAllWithMenuItemsAsync();

            return dbOrders.Select(o => new OrderResponseModel
            {
                Id = o.Id,
                TableId = o.TableId,
                TotalAmount = o.TotalPrice,
                MenuItems = o.OrderItems.Select(mi => new MenuItemResponseModel
                {
                    Id = mi.MenuItem.Id,
                    Name = mi.MenuItem.Name,
                    Price = mi.MenuItem.Price
                }).ToList()
            });
        }
    }
}