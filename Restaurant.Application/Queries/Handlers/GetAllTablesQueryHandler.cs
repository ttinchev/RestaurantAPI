using MediatR;
using Restaurant.Application.Models.Table;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Queries.Handlers
{
    public class GetAllTablesQueryHandler : IRequestHandler<GetAllTablesQuery, IEnumerable<TableResponseModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllTablesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TableResponseModel>> Handle(GetAllTablesQuery request, CancellationToken cancellationToken)
        {
            var dbTables = await _unitOfWork.TableRepository.GetAllAsync();

            return dbTables.Select(t => new TableResponseModel
            {
                Id = t.Id,
                NumberOfSeats = t.NumberOfSeats,
                TotalBill = t.TotalBill,
                IsFree = t.IsFree,
                Enabled = t.Enabled
            });
        }
    }
}