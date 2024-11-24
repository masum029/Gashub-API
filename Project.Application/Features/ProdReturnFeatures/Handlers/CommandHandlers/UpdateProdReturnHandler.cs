using AutoMapper;
using MediatR;
using Project.Application.ApiResponse;
using Project.Application.Exceptions;
using Project.Domail.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Project.Application.Features.ProdReturnFeatures.Handlers.CommandHandlers
{
    public class UpdateProdReturnCommand : IRequest<ApiResponse<string>>
    {
        public Guid Id { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid ProdSizeId { get; set; }
        [Required]
        public Guid ProdValveId { get; set; }
        [Required]
        public string UpdatedBy { get; set; }
        public bool IsConfirmedOrder { get; set; }

    }
    public class UpdateProdReturnHandler : IRequestHandler<UpdateProdReturnCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        public UpdateProdReturnHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }


        public async Task<ApiResponse<string>> Handle(UpdateProdReturnCommand request, CancellationToken cancellationToken)
        {
            // Initialize response object
            var response = new ApiResponse<string>();

            // Retrieve the delivery address by id
            var productReturn = await _unitOfWorkDb.prodReturnQueryRepository.GetByIdAsync(request.Id);

            // Check if the delivery address exists
            if (productReturn == null)
            {
                throw new NotFoundException($"product Return with id = {request.Id} not found");
            }


            try
            {


                // Update delivery address properties

                productReturn.Name = request.Name;
                productReturn.UpdatedBy = request.UpdatedBy;
                productReturn.ProductId = request.ProductId;
                productReturn.ProdValveId = request.ProdValveId;
                productReturn.ProdSizeId = request.ProdSizeId;
                productReturn.IsConfirmedOrder= request.IsConfirmedOrder;   




                // Perform the update operation
                await _unitOfWorkDb.prodReturnCommandRepository.UpdateAsync(productReturn);
                await _unitOfWorkDb.SaveAsync();

                // Set success response
                response.Success = true;
                response.Data = $"product Return with id = {productReturn.Id} updated successfully";
                response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                response.Success = false;
                response.Data = "An error occurred while updating the product Return";
                response.ErrorMessage = ex.Message;
                response.Status = HttpStatusCode.InternalServerError; // Set status code to 500 (Internal Server Error)
            }

            // Return the response
            return response;
        }
    }
    
}