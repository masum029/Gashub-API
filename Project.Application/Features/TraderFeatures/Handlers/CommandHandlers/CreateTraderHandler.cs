using MediatR;
using Project.Application.ApiResponse;
using Project.Domail.Abstractions;
using Project.Domail.Entities;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Project.Application.Features.TraderFeatures.Handlers.CommandHandlers
{
    public class CreateTraderCommand : IRequest<ApiResponse<string>>
    {

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
        public string CreatedBy { get; set; }

    }
    public class CreateTraderHandler : IRequestHandler<CreateTraderCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public CreateTraderHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }


        public async Task<ApiResponse<string>> Handle(CreateTraderCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();

            try
            {

                var newTrader = new Trader
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now.Date,
                    CreatedBy = request.CreatedBy,
                    Name = request.Name,
                    Contactperson = request.Contactperson,
                    ContactPerNum = request.ContactPerNum,
                    ContactNumber = request.ContactNumber,
                    CompanyId= request.CompanyId,
                    BIN = request.BIN,
                    IsActive = true,
                };

                await _unitOfWorkDb.traderCommandRepository.AddAsync(newTrader);
                await _unitOfWorkDb.SaveAsync();
                response.Success = true;
                response.Data = $"Trader id = {newTrader.Id} Created Successfully!";
                response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = "Server Error";
                response.ErrorMessage = ex.Message;
                response.Status = HttpStatusCode.InternalServerError; // Set status code to 500 (Internal Server Error)
            }

            return response;
        }
    }
}
