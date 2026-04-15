using MediatR;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Commands.Handlers
{
    public class DeleteMenuItemCommandHandler : IRequestHandler<DeleteMenuItemCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteMenuItemCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteMenuItemCommand request, CancellationToken cancellationToken)
        {
            var menuItem = await _unitOfWork.MenuItemRepository.GetByIdAsync(request.Id);
            if (menuItem == null)
            {
                return false;
            }

            _unitOfWork.MenuItemRepository.Delete(menuItem);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}