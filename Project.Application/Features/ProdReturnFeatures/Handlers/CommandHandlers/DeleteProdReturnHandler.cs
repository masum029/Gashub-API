using MediatR;
using Project.Application.ApiResponse;
using Project.Application.Exceptions;
using Project.Domail.Abstractions;
using System.Net;

namespace Project.Application.Features.ProdReturnFeatures.Handlers.CommandHandlers
{
    public class DeleteProdReturnCommand : IRequest<ApiResponse<string>>
    {
        public DeleteProdReturnCommand(Guid id)
        {
            this.id = id;
        }

        public Guid id { get; private set; }
    }
    public class DeleteProdReturnHandler : IRequestHandler<DeleteProdReturnCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public DeleteProdReturnHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }


        public async Task<ApiResponse<string>> Handle(DeleteProdReturnCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();
            var productReturn = await _unitOfWorkDb.prodReturnQueryRepository.GetByIdAsync(request.id);

            if (productReturn == null)
            {
                throw new NotFoundException($"product Return with id = {request.id} not found");
            }
            else
            {
                try
                {
                    await _unitOfWorkDb.prodReturnCommandRepository.DeleteAsync(productReturn);
                    await _unitOfWorkDb.SaveAsync();

                    response.Success = true;
                    response.Data = $"product Return with id = {productReturn.Id} deleted successfully";
                    response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Data = "An error occurred while deleting the product Return ";
                    response.ErrorMessage = ex.Message;
                    response.Status = HttpStatusCode.InternalServerError;  // Set status code to 500 (Internal Server Error)
                }

            }
            return response;
        }
    }
    
}
