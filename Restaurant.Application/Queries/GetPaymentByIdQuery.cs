using MediatR;
using Restaurant.Application.Models.Payment;

namespace Restaurant.Application.Queries
{
    public class GetPaymentByIdQuery(int id) : IRequest<PaymentResponseModel>
    {
        public int Id { get; set; } = id;
    }
}