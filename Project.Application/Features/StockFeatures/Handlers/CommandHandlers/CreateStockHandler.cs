using MediatR;
using Project.Application.ApiResponse;
using Project.Domail.Abstractions;
using Project.Domail.Entities;
using System.ComponentModel.DataAnnotations;
using System.Net;


namespace Project.Application.Features.StockFeatures.Handlers.CommandHandlers
{
    public class CreateStockCommand : IRequest<ApiResponse<string>>
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public Guid TraderId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string CreatedBy { get; set; }
    }
    public class CreateStockHandler : IRequestHandler<CreateStockCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public CreateStockHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }
 
        public async Task<ApiResponse<string>> Handle(CreateStockCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();

            try
            {
                var newStock = new Stock
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now.Date,
                    CreatedBy = request.CreatedBy,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    TraderId= request.TraderId,
                    IsActive = true,
                    IsQC=true,
                };

                await _unitOfWorkDb.stockCommandRepository.AddAsync(newStock);
                await _unitOfWorkDb.SaveAsync();

                response.Success = true;
                response.Data = $" Stock  id = {newStock.Id} created successfully!";
                response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = "An error occurred while creating the Stock ";
                response.ErrorMessage = ex.Message;
                response.Status = HttpStatusCode.InternalServerError; // Set status code to 500 (Internal Server Error)
            }

            return response;
        }
    }
}
