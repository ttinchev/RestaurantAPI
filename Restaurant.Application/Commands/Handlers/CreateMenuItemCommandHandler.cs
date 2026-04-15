using MediatR;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Commands.Handlers
{
    public class CreateMenuItemCommandHandler : IRequestHandler<CreateMenuItemCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateMenuItemCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateMenuItemCommand request, CancellationToken cancellationToken)
        {
            var dbMenuItem = new MenuItem
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CategoryId = request.CategoryId
            };

            await _unitOfWork.MenuItemRepository.AddAsync(dbMenuItem);
            await _unitOfWork.SaveChangesAsync();
            return dbMenuItem.Id;
        }
    }
}