using MediatR;

namespace Restaurant.Application.Commands
{
    public class DeleteCategoryCommand(int id) : IRequest<bool>
    {
        public int Id { get; set; } = id;
    }
}