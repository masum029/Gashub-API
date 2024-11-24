using MediatR;
using Project.Application.ApiResponse;
using Project.Domail.Abstractions;
using Project.Domail.Entities;
using System.ComponentModel.DataAnnotations;
using System.Net;


namespace Project.Application.Features.ProductFeatures.Handlers.CommandHandlers
{
    public class CreateProductCommand : IRequest<ApiResponse<string>>
    {
        [Required]
        public Guid CompanyId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string Name { get; set; }
        [Required]
        public Guid ProdSizeId { get; set; }
        [Required]
        public Guid ProdValveId { get; set; }
        public string ProdImage { get; set; }
        [Required]
        public int ProdPrice { get; set; }
        [Required]
        public string CreatedBy { get; set; }
    }
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public CreateProductHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }

        public async Task<ApiResponse<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();

            try
            {
                var ExjistingProductList = await _unitOfWorkDb.productQueryRepository.GetAllAsync();
                foreach (var product in ExjistingProductList)
                {
                    if (product?.Name?.Trim() == request.Name.Trim())
                    {
                        response.Success = false;
                        response.Data = "An error occurred while creating the Product";
                        response.ErrorMessage = "DuplicateProductName: Product name already exists.";
                        response.Status = HttpStatusCode.InternalServerError;

                        return response;
                    }
                    if (product?.ProdSizeId == request.ProdSizeId && product?.CompanyId == request.CompanyId)
                    {
                        response.Success = false;
                        response.Data = "An error occurred while creating the Product";
                        response.ErrorMessage = "DuplicateProductSize: Product Size already exists.";
                        response.Status = HttpStatusCode.InternalServerError;

                        return response;
                    }
                }
                var newProduct = new Product
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now.Date,
                    CreatedBy = request.CreatedBy,
                    Name = request.Name,
                    ProdSizeId = request.ProdSizeId,
                    ProdValveId = request.ProdValveId,
                    CompanyId= request.CompanyId,
                    ProdImage = request.ProdImage,
                    ProdPrice = request.ProdPrice,
                };

                await _unitOfWorkDb.productCommandRepository.AddAsync(newProduct);
                await _unitOfWorkDb.SaveAsync();

                response.Success = true;
                response.Data = $" Product  id = {newProduct.Id} created successfully!";
                response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = "An error occurred while creating the Product ";
                response.ErrorMessage = ex.Message;
                response.Status = HttpStatusCode.InternalServerError; // Set status code to 500 (Internal Server Error)
            }

            return response;
        }
    }
}
