using MediatR;
using Project.Application.ApiResponse;
using Project.Domail.Abstractions;
using Project.Domail.Entities;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Project.Application.Features.DeliveryAddressFeatures.Handlers.CommandHandlers
{
    public class CreateDeliveryAddressCommand : IRequest<ApiResponse<string>>
    {
        [Required(ErrorMessage = "UserId is required.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 50 characters.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [RegularExpression("^[0-9]{11}$", ErrorMessage = "Phone must be 11 digits.")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Mobile is required.")]
        [RegularExpression("^[0-9]{11}$", ErrorMessage = "Mobile must be 11 digits.")]
        public string? Mobile { get; set; }

        public string? CreatedBy { get; set; }

    }
    public class CreateDeliveryAddressHandler : IRequestHandler<CreateDeliveryAddressCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public CreateDeliveryAddressHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
            
        }
        public async Task<ApiResponse<string>> Handle(CreateDeliveryAddressCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();

            try
            {
                var newDelivaryAddress = new DeliveryAddress
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now.Date,
                    CreatedBy = request.CreatedBy,
                    UserId = request.UserId,
                    Phone = request.Phone,
                    Mobile = request.Mobile,
                    Address = request.Address,
                    IsActive = true,
                    IsDefault = true,
                };

                await _unitOfWorkDb.deliveryAddressCommandRepository.AddAsync(newDelivaryAddress);
                await _unitOfWorkDb.SaveAsync();

                response.Success = true;
                response.Data = $"Delivery Address id = {newDelivaryAddress.Id} created successfully!";
                response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = "An error occurred while creating the delivery address";
                response.ErrorMessage = ex.Message;
                response.Status = HttpStatusCode.InternalServerError; // Set status code to 500 (Internal Server Error)
            }

            return response;
        }

    }
}
