using MediatR;

namespace Restaurant.Application.Commands
{
    public class DeleteOrderCommand(int id) : IRequest<bool>
    {
        public int Id { get; set; } = id;
    }
}