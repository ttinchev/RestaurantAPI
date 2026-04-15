using MediatR;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Commands.Handlers
{
    public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdatePaymentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(request.Id);
            if (payment == null)
            {
                throw new Exception("Payment not found");
            }

            payment.Amount = request.Amount;
            payment.IsSuccessful = request.IsSuccessful;
            payment.PaymentMethod = request.PaymentMethod;
            payment.TableId = request.TableId;

            _unitOfWork.PaymentRepository.Update(payment);
            await _unitOfWork.SaveChangesAsync();
            return payment.Id;
        }
    }
}