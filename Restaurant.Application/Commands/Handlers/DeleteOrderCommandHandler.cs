using MediatR;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Commands.Handlers
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.OrdersRepository.GetByIdAsync(request.Id);
            if (order == null)
            {
                return false;
            }

            _unitOfWork.OrdersRepository.Delete(order);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}