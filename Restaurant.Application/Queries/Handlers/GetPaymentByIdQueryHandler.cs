using MediatR;
using Restaurant.Application.Models.Payment;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Queries.Handlers
{
    public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, PaymentResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPaymentByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaymentResponseModel> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            var dbPayment = await _unitOfWork.PaymentRepository.GetByIdAsync(request.Id);

            var result = new PaymentResponseModel();

            if (dbPayment != null)
            {
                result.Id = dbPayment.Id;
                result.Amount = dbPayment.Amount;
                result.IsSuccessful = dbPayment.IsSuccessful;
                result.PaymentMethod = dbPayment.PaymentMethod;
                result.TableId = dbPayment.TableId;
            }

            return result;
        }
    }
}