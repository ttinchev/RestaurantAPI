using MediatR;
using Restaurant.Application.Models.Table;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Queries.Handlers
{
    public class GetTableByIdQueryHandler : IRequestHandler<GetTableByIdQuery, TableResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetTableByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TableResponseModel> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
        {
            var dbTable = await _unitOfWork.TableRepository.GetByIdAsync(request.Id);

            var result = new TableResponseModel();

            if (dbTable != null)
            {
                result.Id = dbTable.Id;
                result.NumberOfSeats = dbTable.NumberOfSeats;
                result.TotalBill = dbTable.TotalBill;
                result.IsFree = dbTable.IsFree;
                result.Enabled = dbTable.Enabled;
            }

            return result;
        }
    }
}