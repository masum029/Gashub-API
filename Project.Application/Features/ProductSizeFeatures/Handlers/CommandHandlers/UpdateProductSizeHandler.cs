using MediatR;
using Project.Application.ApiResponse;
using Project.Application.Exceptions;
using Project.Domail.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Project.Application.Features.ProductSizeFeatures.Handlers.CommandHandlers
{
    public class UpdateProductSizeCommand : IRequest<ApiResponse<string>>
    {
        public Guid Id { get; set; }
        [Required]
        public decimal Size { get; set; }
        [Required]
        public string? Unit { get; set; }
        [Required]
        public string? UpdatedBy { get; set; }
    }
    public class UpdateProductSizeHandler : IRequestHandler<UpdateProductSizeCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        public UpdateProductSizeHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }
        public async Task<ApiResponse<string>> Handle(UpdateProductSizeCommand request, CancellationToken cancellationToken)
        {
            // Initialize response object
            var response = new ApiResponse<string>();

            // Retrieve the delivery address by id
            var productSize = await _unitOfWorkDb.productSizeQueryRepository.GetByIdAsync(request.Id);

            // Check if the delivery address exists
            if (productSize == null)
            {
                throw new NotFoundException($"product Size  with id = {request.Id} not found");
            }


            try
            {


                // Update delivery address properties
                productSize.UpdatedBy = request.UpdatedBy;
                productSize.Size = request.Size;
                productSize.Unit = request.Unit;





                // Perform the update operation
                await _unitOfWorkDb.productSizeCommandRepository.UpdateAsync(productSize);
                await _unitOfWorkDb.SaveAsync();

                // Set success response
                response.Success = true;
                response.Data = $"product Size  with id = {productSize.Id} updated successfully";
                response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                response.Success = false;
                response.Data = "An error occurred while updating the product Size ";
                response.ErrorMessage = ex.Message;
                response.Status = HttpStatusCode.InternalServerError; // Set status code to 500 (Internal Server Error)
            }

            // Return the response
            return response;
        }
    }
}
