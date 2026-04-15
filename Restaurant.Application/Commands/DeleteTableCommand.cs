using MediatR;

namespace Restaurant.Application.Commands
{
    public class DeleteTableCommand(int id) : IRequest<bool>
    {
        public int Id { get; set; } = id;
    }
}