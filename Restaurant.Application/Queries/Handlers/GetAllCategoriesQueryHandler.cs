using MediatR;
using Restaurant.Application.Models.Category;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Queries.Handlers
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryResponseModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CategoryResponseModel>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var dbCategories = await _unitOfWork.CategoryRepository.GetAllAsync();

            return dbCategories.Select(c => new CategoryResponseModel
            {
                Id = c.Id,
                Name = c.Name
            });
        }
    }
}