using MediatR;
using Project.Application.ApiResponse;
using Project.Application.Exceptions;
using Project.Domail.Abstractions;
using System.Net;

namespace Project.Application.Features.ProductSizeFeatures.Handlers.CommandHandlers
{
    public class DeleteProductSizeCommand : IRequest<ApiResponse<string>>
    {
        public DeleteProductSizeCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
    internal class DeleteProductSizeHandler : IRequestHandler<DeleteProductSizeCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public DeleteProductSizeHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }

        public async Task<ApiResponse<string>> Handle(DeleteProductSizeCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();
            var productSize = await _unitOfWorkDb.productSizeQueryRepository.GetByIdAsync(request.Id);

            if (productSize == null)
            {
                throw new NotFoundException($"product Size  with id = {request.Id} not found");
            }
            else
            {
                try
                {
                    await _unitOfWorkDb.productSizeCommandRepository.DeleteAsync(productSize);
                    await _unitOfWorkDb.SaveAsync();

                    response.Success = true;
                    response.Data = $"product Size  with id = {productSize.Id} deleted successfully";
                    response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Data = "An error occurred while deleting the product Size  ";
                    response.ErrorMessage = ex.Message;
                    response.Status = HttpStatusCode.InternalServerError;  // Set status code to 500 (Internal Server Error)
                }

            }
            return response;
        }
    }
}
