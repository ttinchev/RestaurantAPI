using MediatR;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Commands.Handlers
{
    public class CreateTableCommandHandler : IRequestHandler<CreateTableCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateTableCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateTableCommand request, CancellationToken cancellationToken)
        {
            var dbTable = new Table
            {
                NumberOfSeats = request.NumberOfSeats,
                IsFree = request.IsFree,
                Enabled = request.Enabled
            };

            await _unitOfWork.TableRepository.AddAsync(dbTable);
            await _unitOfWork.SaveChangesAsync();
            return dbTable.Id;
        }
    }
}