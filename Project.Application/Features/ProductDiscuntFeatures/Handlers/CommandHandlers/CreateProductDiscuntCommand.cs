using MediatR;
using Project.Application.ApiResponse;
using Project.Domail.Abstractions;
using Project.Domail.Entities;
using System.ComponentModel.DataAnnotations;
using System.Net;


namespace Project.Application.Features.ProductDiscuntFeatures.Handlers.CommandHandlers
{
    public class CreateProductDiscuntCommand : IRequest<ApiResponse<string>>
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public decimal DiscountedPrice { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTime ValidTill { get; set; }
        [Required]
        public string CreatedBy { get; set; }
    }
    public class CreateProductDiscuntHandler : IRequestHandler<CreateProductDiscuntCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public CreateProductDiscuntHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }

        public async Task<ApiResponse<string>> Handle(CreateProductDiscuntCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();

            try
            {
                var newProductDiscunt = new ProductDiscunt
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now.Date,
                    CreatedBy = request.CreatedBy,
                    ProductId = request.ProductId,
                    DiscountedPrice = request.DiscountedPrice,
                    IsActive = true,
                    ValidTill = request.ValidTill,

                };

                await _unitOfWorkDb.productDiscuntCommandRepository.AddAsync(newProductDiscunt);
                await _unitOfWorkDb.SaveAsync();

                response.Success = true;
                response.Data = $" Product Discount id = {newProductDiscunt.Id} created successfully!";
                response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = "An error occurred while creating the Product Discount";
                response.ErrorMessage = ex.Message;
                response.Status = HttpStatusCode.InternalServerError; // Set status code to 500 (Internal Server Error)
            }

            return response;
        }
    }
}
