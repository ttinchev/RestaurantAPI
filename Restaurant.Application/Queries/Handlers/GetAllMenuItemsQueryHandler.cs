using MediatR;
using Restaurant.Application.Models.MenuItem;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Queries.Handlers
{
    public class GetAllMenuItemsQueryHandler : IRequestHandler<GetAllMenuItemsQuery, IEnumerable<MenuItemResponseModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllMenuItemsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<MenuItemResponseModel>> Handle(GetAllMenuItemsQuery request, CancellationToken cancellationToken)
        {
            var dbMenuItems = await _unitOfWork.MenuItemRepository.GetAllAsync();

            return dbMenuItems.Select(mi => new MenuItemResponseModel
            {
                Id = mi.Id,
                Name = mi.Name,
                Description = mi.Description,
                Price = mi.Price,
                CategoryId = mi.CategoryId
            });
        }
    }
}