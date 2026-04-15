using MediatR;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Commands.Handlers
{
    public class UpdateMenuItemCommandHandler : IRequestHandler<UpdateMenuItemCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateMenuItemCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
        {
            var menuItem = await _unitOfWork.MenuItemRepository.GetByIdAsync(request.Id);
            if (menuItem == null)
            {
                throw new Exception("MenuItem not found");
            }

            menuItem.Name = request.Name;
            menuItem.Description = request.Description;
            menuItem.Price = request.Price;
            menuItem.CategoryId = request.CategoryId;

            _unitOfWork.MenuItemRepository.Update(menuItem);
            await _unitOfWork.SaveChangesAsync();
            return menuItem.Id;
        }
    }
}