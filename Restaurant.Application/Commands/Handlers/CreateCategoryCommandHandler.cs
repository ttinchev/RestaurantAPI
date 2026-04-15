using MediatR;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Commands.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var dbCategory = new Category
            {
                Name = request.Name
            };

            await _unitOfWork.CategoryRepository.AddAsync(dbCategory);
            await _unitOfWork.SaveChangesAsync();
            return dbCategory.Id;
        }
    }
}