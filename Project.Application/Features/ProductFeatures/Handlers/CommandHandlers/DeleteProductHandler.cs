using MediatR;
using Project.Application.ApiResponse;
using Project.Application.Exceptions;
using Project.Domail.Abstractions;
using System.Net;


namespace Project.Application.Features.ProductFeatures.Handlers.CommandHandlers
{
    public class DeleteProductCommand : IRequest<ApiResponse<string>>
    {
        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public DeleteProductHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }


        public async Task<ApiResponse<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();
            var product = await _unitOfWorkDb.productQueryRepository.GetByIdAsync(request.Id);

            if (product == null)
            {
                throw new NotFoundException($"product  with id = {request.Id} not found");
            }
            else
            {
                try
                {
                    await _unitOfWorkDb.productCommandRepository.DeleteAsync(product);
                    await _unitOfWorkDb.SaveAsync();

                    response.Success = true;
                    response.Data = $"product  with id = {product.Id} deleted successfully";
                    response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Data = "An error occurred while deleting the product  ";
                    response.ErrorMessage = ex.Message;
                    response.Status = HttpStatusCode.InternalServerError;  // Set status code to 500 (Internal Server Error)
                }

            }
            return response;
        }
    }
}
