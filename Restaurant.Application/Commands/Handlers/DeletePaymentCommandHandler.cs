using MediatR;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Commands.Handlers
{
    public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeletePaymentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(request.Id);
            if (payment == null)
            {
                return false;
            }

            _unitOfWork.PaymentRepository.Delete(payment);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}