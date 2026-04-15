using MediatR;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Commands.Handlers
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);
            if (category == null)
            {
                return false;
            }

            _unitOfWork.CategoryRepository.Delete(category);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}