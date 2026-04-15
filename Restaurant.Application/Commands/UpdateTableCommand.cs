using MediatR;

namespace Restaurant.Application.Commands
{
    public class UpdateTableCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int NumberOfSeats { get; set; }
        public bool IsFree { get; set; }
        public bool Enabled { get; set; }
    }
}