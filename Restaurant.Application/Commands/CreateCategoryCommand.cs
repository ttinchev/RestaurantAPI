using MediatR;

namespace Restaurant.Application.Commands
{
    public class CreateCategoryCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
    }
}