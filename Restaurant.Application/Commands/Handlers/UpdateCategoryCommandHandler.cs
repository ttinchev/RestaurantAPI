using MediatR;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Commands.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

            category.Name = request.Name;
            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync();
            return category.Id;
        }
    }
}