using MediatR;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Commands.Handlers
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreatePaymentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var dbPayment = new Payment
            {
                Amount = request.Amount,
                PaymentMethod = request.PaymentMethod,
                TableId = request.TableId,
                IsSuccessful = false
            };

            await _unitOfWork.PaymentRepository.AddAsync(dbPayment);
            await _unitOfWork.SaveChangesAsync();
            return dbPayment.Id;
        }
    }
}