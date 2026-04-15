using MediatR;
using Restaurant.Application.Models.MenuItem;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Queries.Handlers
{
    public class GetMenuItemByIdQueryHandler : IRequestHandler<GetMenuItemByIdQuery, MenuItemResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetMenuItemByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MenuItemResponseModel> Handle(GetMenuItemByIdQuery request, CancellationToken cancellationToken)
        {
            var dbMenuItem = await _unitOfWork.MenuItemRepository.GetByIdAsync(request.Id);

            var result = new MenuItemResponseModel();

            if (dbMenuItem != null)
            {
                result.Id = dbMenuItem.Id;
                result.Name = dbMenuItem.Name;
                result.Description = dbMenuItem.Description;
                result.Price = dbMenuItem.Price;
                result.CategoryId = dbMenuItem.CategoryId;
            }

            return result;
        }
    }
}