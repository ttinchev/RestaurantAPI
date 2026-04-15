using MediatR;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Commands.Handlers
{
    public class UpdateTableCommandHandler : IRequestHandler<UpdateTableCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateTableCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
        {
            var table = await _unitOfWork.TableRepository.GetByIdAsync(request.Id);
            if (table == null)
            {
                throw new Exception("Table not found");
            }

            table.NumberOfSeats = request.NumberOfSeats;
            table.IsFree = request.IsFree;
            table.Enabled = request.Enabled;

            _unitOfWork.TableRepository.Update(table);
            await _unitOfWork.SaveChangesAsync();
            return table.Id;
        }
    }
}