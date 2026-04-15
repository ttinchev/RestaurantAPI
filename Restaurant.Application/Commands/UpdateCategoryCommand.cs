using MediatR;

namespace Restaurant.Application.Commands
{
    public class UpdateCategoryCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}