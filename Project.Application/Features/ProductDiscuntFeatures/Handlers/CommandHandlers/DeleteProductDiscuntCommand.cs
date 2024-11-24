using MediatR;
using Project.Application.ApiResponse;
using Project.Application.Exceptions;
using Project.Domail.Abstractions;
using System.Net;


namespace Project.Application.Features.ProductDiscuntFeatures.Handlers.CommandHandlers
{
    public class DeleteProductDiscuntCommand : IRequest<ApiResponse<string>>
    {
        public DeleteProductDiscuntCommand(Guid id)
        {
            this.id = id;
        }

        public Guid id { get; private set; }
    }
    public class DeleteProductDiscuntHandler : IRequestHandler<DeleteProductDiscuntCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public DeleteProductDiscuntHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }


        public async Task<ApiResponse<string>> Handle(DeleteProductDiscuntCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();
            var productDiscount = await _unitOfWorkDb.productDiscuntQueryRepository.GetByIdAsync(request.id);

            if (productDiscount == null)
            {
                throw new NotFoundException($"product Discount with id = {request.id} not found");
            }
            else
            {
                try
                {
                    await _unitOfWorkDb.productDiscuntCommandRepository.DeleteAsync(productDiscount);
                    await _unitOfWorkDb.SaveAsync();

                    response.Success = true;
                    response.Data = $"product Discount with id = {productDiscount.Id} deleted successfully";
                    response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Data = "An error occurred while deleting the product Discount ";
                    response.ErrorMessage = ex.Message;
                    response.Status = HttpStatusCode.InternalServerError;  // Set status code to 500 (Internal Server Error)
                }

            }
            return response;
        }
    }
}
