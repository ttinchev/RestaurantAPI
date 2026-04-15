using MediatR;

namespace Restaurant.Application.Commands
{
    public class DeleteMenuItemCommand(int id) : IRequest<bool>
    {
        public int Id { get; set; } = id;
    }
}