using MediatR;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Commands.Handlers
{
    public class DeleteTableCommandHandler : IRequestHandler<DeleteTableCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteTableCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteTableCommand request, CancellationToken cancellationToken)
        {
            var table = await _unitOfWork.TableRepository.GetByIdAsync(request.Id);
            if (table == null)
            {
                return false;
            }

            _unitOfWork.TableRepository.Delete(table);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}