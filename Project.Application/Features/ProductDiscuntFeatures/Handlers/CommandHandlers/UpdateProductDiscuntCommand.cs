using MediatR;
using Project.Application.ApiResponse;
using Project.Application.Exceptions;
using Project.Application.Features.ProdReturnFeatures.Handlers.CommandHandlers;
using Project.Domail.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.Net;


namespace Project.Application.Features.ProductDiscuntFeatures.Handlers.CommandHandlers
{
    public class UpdateProductDiscuntCommand : IRequest<ApiResponse<string>>
    {
        public Guid Id { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public decimal DiscountedPrice { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTime ValidTill { get; set; }
        [Required]
        public string UpdatedBy { get; set; }

    }
    public class UpdateProductDiscuntHandler : IRequestHandler<UpdateProductDiscuntCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        public UpdateProductDiscuntHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }


        public async Task<ApiResponse<string>> Handle(UpdateProductDiscuntCommand request, CancellationToken cancellationToken)
        {
            // Initialize response object
            var response = new ApiResponse<string>();

            // Retrieve the delivery address by id
            var productDiscount = await _unitOfWorkDb.productDiscuntQueryRepository.GetByIdAsync(request.Id);

            // Check if the delivery address exists
            if (productDiscount == null)
            {
                throw new NotFoundException($"product Discount with id = {request.Id} not found");
            }


            try
            {


                // Update delivery address properties
                productDiscount.UpdatedBy = request.UpdatedBy;
                productDiscount.ProductId = request.ProductId;
                productDiscount.ValidTill = request.ValidTill;
                productDiscount.IsActive = request.IsActive;
                productDiscount.DiscountedPrice = request.DiscountedPrice;





                // Perform the update operation
                await _unitOfWorkDb.productDiscuntCommandRepository.UpdateAsync(productDiscount);
                await _unitOfWorkDb.SaveAsync();

                // Set success response
                response.Success = true;
                response.Data = $"product Discount with id = {productDiscount.Id} updated successfully";
                response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                response.Success = false;
                response.Data = "An error occurred while updating the product Discount";
                response.ErrorMessage = ex.Message;
                response.Status = HttpStatusCode.InternalServerError; // Set status code to 500 (Internal Server Error)
            }

            // Return the response
            return response;
        }
    }
}
