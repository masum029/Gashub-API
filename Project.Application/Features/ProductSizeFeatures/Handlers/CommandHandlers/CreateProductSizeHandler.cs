using MediatR;
using Project.Application.ApiResponse;
using Project.Domail.Abstractions;
using Project.Domail.Entities;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Project.Application.Features.ProductSizeFeatures.Handlers.CommandHandlers
{
    public class CreateProductSizeCommand : IRequest<ApiResponse<string>>
    {
        [Required]
        public decimal Size { get; set; }
        [Required]
        public string? Unit { get; set; }
        [Required]
        public string? CreatedBy { get; set; }
    }
    public class CreateProductSizeHandler : IRequestHandler<CreateProductSizeCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public CreateProductSizeHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }

        public async Task<ApiResponse<string>> Handle(CreateProductSizeCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();

            try
            {
                var ExjistingProductSizeList = await _unitOfWorkDb.productSizeQueryRepository.GetAllAsync();
                foreach (var productSize in ExjistingProductSizeList)
                {
                    if (productSize?.Size == request.Size)
                    {
                        response.Success = false;
                        response.Data = "An error occurred while creating the Product Size";
                        response.ErrorMessage = "DuplicateProductSize: Product  Size  already exists.";
                        response.Status = HttpStatusCode.InternalServerError;

                        return response;
                    }
                }
                var newProductSize = new ProductSize
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now.Date,
                    CreatedBy = request.CreatedBy,
                    Size= request.Size,
                    Unit= request.Unit,
                };

                await _unitOfWorkDb.productSizeCommandRepository.AddAsync(newProductSize);
                await _unitOfWorkDb.SaveAsync();

                response.Success = true;
                response.Data = $" Product Size  id = {newProductSize.Id} created successfully!";
                response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = "An error occurred while creating the Product Size ";
                response.ErrorMessage = ex.Message;
                response.Status = HttpStatusCode.InternalServerError; // Set status code to 500 (Internal Server Error)
            }

            return response;
        }
    }
}
