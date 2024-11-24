using MediatR;
using Project.Application.ApiResponse;
using Project.Domail.Abstractions;
using Project.Domail.Entities;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Project.Application.Features.ProdReturnFeatures.Handlers.CommandHandlers
{
    public class CreateProdReturnCommand : IRequest<ApiResponse<string>>
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required(ErrorMessage ="Name Is required")]
        public string Name { get; set; }
        [Required]
        public Guid ProdSizeId { get; set; }
        [Required]
        public Guid ProdValveId { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        public bool IsConfirmedOrder { get; set; }


    }
    public class CreateProdReturnHandler : IRequestHandler<CreateProdReturnCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public CreateProdReturnHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }

        public async Task<ApiResponse<string>> Handle(CreateProdReturnCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();

            try
            {
                var newProdReturn = new ProdReturn
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now.Date,
                    CreatedBy = request.CreatedBy,
                    ProdSizeId = request.ProdSizeId,
                    ProductId = request.ProductId,
                    Name=request.Name,
                    ProdValveId=request.ProdValveId,
                    IsConfirmedOrder= false,

                };

                await _unitOfWorkDb.prodReturnCommandRepository.AddAsync(newProdReturn);
                await _unitOfWorkDb.SaveAsync();

                response.Success = true;
                response.Data = $" Product Return id = {newProdReturn.Id} created successfully!";
                response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = "An error occurred while creating the Product Return";
                response.ErrorMessage = ex.Message;
                response.Status = HttpStatusCode.InternalServerError; // Set status code to 500 (Internal Server Error)
            }

            return response;
        }
    }
    
}
