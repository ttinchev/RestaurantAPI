using MediatR;

namespace Restaurant.Application.Commands
{
    public class CreateTableCommand : IRequest<int>
    {
        public int NumberOfSeats { get; set; }
        public bool IsFree { get; set; }
        public bool Enabled { get; set; }
    }
}