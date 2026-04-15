using MediatR;

namespace Restaurant.Application.Commands
{
    public class DeletePaymentCommand(int id) : IRequest<bool>
    {
        public int Id { get; set; } = id;
    }
}