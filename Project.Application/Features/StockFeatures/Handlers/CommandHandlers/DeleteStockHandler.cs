using MediatR;
using Project.Application.ApiResponse;
using Project.Application.Exceptions;
using Project.Domail.Abstractions;
using System.Net;


namespace Project.Application.Features.StockFeatures.Handlers.CommandHandlers
{
    public class DeleteStockCommand : IRequest<ApiResponse<string>>
    {
        public DeleteStockCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
    public class DeleteStockHandler : IRequestHandler<DeleteStockCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public DeleteStockHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }

        public async Task<ApiResponse<string>> Handle(DeleteStockCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();
            var stock = await _unitOfWorkDb.stockQueryRepository.GetByIdAsync(request.Id);

            if (stock == null)
            {
                throw new NotFoundException($"stock with id = {request.Id} not found");
            }
            else
            {
                try
                {
                    await _unitOfWorkDb.stockCommandRepository.DeleteAsync(stock);
                    await _unitOfWorkDb.SaveAsync();

                    response.Success = true;
                    response.Data = $"stock with id = {stock.Id} deleted successfully";
                    response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Data = "An error occurred while deleting the stock  ";
                    response.ErrorMessage = ex.Message;
                    response.Status = HttpStatusCode.InternalServerError;  // Set status code to 500 (Internal Server Error)
                }

            }
            return response;
        }
    }
}
