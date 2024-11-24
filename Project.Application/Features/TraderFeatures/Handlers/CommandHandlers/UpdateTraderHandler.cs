using MediatR;
using Project.Application.ApiResponse;
using Project.Application.Exceptions;
using Project.Domail.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.Net;


namespace Project.Application.Features.TraderFeatures.Handlers.CommandHandlers
{
    public class UpdateTraderCommand : IRequest<ApiResponse<string>>
    {
        public Guid Id { get; set; }
        [Required]
        public Guid CompanyId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Contact Person is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Contact Person must be between 2 and 50 characters.")]
        public string Contactperson { get; set; }

        [Required(ErrorMessage = "Contact Person Number is required.")]
        [RegularExpression("^[0-9]{11}$", ErrorMessage = "Contact Person Number must be 11 digits.")]
        public string ContactPerNum { get; set; }

        [Required(ErrorMessage = "Contact Number is required.")]
        [RegularExpression("^[0-9]{11}$", ErrorMessage = "Contact  Number must be 11 digits.")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "BIN is required.")]
        public string BIN { get; set; }
        public string UpdatedBy { get; set; }

    }
    public class UpdateTraderHandler : IRequestHandler<UpdateTraderCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        public UpdateTraderHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }

        public async Task<ApiResponse<string>> Handle(UpdateTraderCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();

            var trader = await _unitOfWorkDb.traderQueryRepository.GetByIdAsync(request.Id);

            if (trader == null || trader.Id != request.Id)
            {
                throw new NotFoundException($"trader with id = {request.Id} not found");
            }

            try
            {
                // Update company properties
                trader.BIN = request.BIN;
                trader.Name = request.Name;
                trader.Contactperson = request.Contactperson;
                trader.ContactPerNum = request.ContactPerNum;
                trader.ContactNumber = request.ContactNumber;
                trader.CompanyId= request.CompanyId;
                trader.UpdatedBy = request.UpdatedBy;

                // Perform update operation
                await _unitOfWorkDb.traderCommandRepository.UpdateAsync(trader);
                await _unitOfWorkDb.SaveAsync();

                // Map the updated company to your DTO model if needed
                response.Success = true;
                response.Data = $"trader with id = {trader.Id} updated successfully";
                response.Status = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                response.Success = false;
                response.Data = "An error occurred while updating the trader";
                response.ErrorMessage = ex.Message;
                response.Status = HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}
