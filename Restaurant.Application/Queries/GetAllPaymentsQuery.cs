using MediatR;
using Restaurant.Application.Models.Payment;

namespace Restaurant.Application.Queries
{
    public class GetAllPaymentsQuery : IRequest<IEnumerable<PaymentResponseModel>>
    {
    }
}