using MediatR;
using Restaurant.Application.Models.Payment;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Queries.Handlers
{
    public class GetAllPaymentsQueryHandler : IRequestHandler<GetAllPaymentsQuery, IEnumerable<PaymentResponseModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllPaymentsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PaymentResponseModel>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
        {
            var dbPayments = await _unitOfWork.PaymentRepository.GetAllAsync();

            return dbPayments.Select(p => new PaymentResponseModel
            {
                Id = p.Id,
                Amount = p.Amount,
                IsSuccessful = p.IsSuccessful,
                PaymentMethod = p.PaymentMethod,
                TableId = p.TableId
            });
        }
    }
}