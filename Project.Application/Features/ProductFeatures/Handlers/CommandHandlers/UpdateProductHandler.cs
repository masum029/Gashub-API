using MediatR;
using Project.Application.ApiResponse;
using Project.Application.Exceptions;
using Project.Domail.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.Net;


namespace Project.Application.Features.ProductFeatures.Handlers.CommandHandlers
{
    public class UpdateProductCommand : IRequest<ApiResponse<string>>
    {
        public Guid Id { get; set; }
        [Required]
        public Guid CompanyId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string Name { get; set; }
        [Required]
        public Guid ProdSizeId { get; set; }
        [Required]
        public Guid ProdValveId { get; set; }
        [Required]

        public string ProdImage { get; set; }
        [Required]
        public int ProdPrice { get; set; }
        [Required]
        public string UpdatedBy { get; set; }
    }
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        public UpdateProductHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }

        public async Task<ApiResponse<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            // Initialize response object
            var response = new ApiResponse<string>();

            // Retrieve the delivery address by id
            var product = await _unitOfWorkDb.productQueryRepository.GetByIdAsync(request.Id);

            // Check if the delivery address exists
            if (product == null)
            {
                throw new NotFoundException($"product  with id = {request.Id} not found");
            }


            try
            {


                // Update delivery address properties

                product.Name = request.Name;
                product.UpdatedBy = request.UpdatedBy;
                product.CompanyId = request.CompanyId;
                product.ProdValveId = request.ProdValveId;
                product.ProdSizeId = request.ProdSizeId;
                if(request.ProdImage != null)
                {
                  product.ProdImage = request.ProdImage;
                }
                product.ProdPrice = request.ProdPrice;




                // Perform the update operation
                await _unitOfWorkDb.productCommandRepository.UpdateAsync(product);
                await _unitOfWorkDb.SaveAsync();

                // Set success response
                response.Success = true;
                response.Data = $"product  with id = {product.Id} updated successfully";
                response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                response.Success = false;
                response.Data = "An error occurred while updating the product ";
                response.ErrorMessage = ex.Message;
                response.Status = HttpStatusCode.InternalServerError; // Set status code to 500 (Internal Server Error)
            }

            // Return the response
            return response;
        }
    }
}