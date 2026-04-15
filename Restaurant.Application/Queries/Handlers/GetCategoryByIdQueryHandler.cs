using MediatR;
using Restaurant.Application.Models.Category;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Queries.Handlers
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryResponseModel> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var dbCategory = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);

            var result = new CategoryResponseModel();

            if (dbCategory != null)
            {
                result.Id = dbCategory.Id;
                result.Name = dbCategory.Name;
            }

            return result;
        }
    }
}