using AutoMapper;
using MediatR;
using Project.Application.ApiResponse;
using Project.Application.Exceptions;
using Project.Domail.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Project.Application.Features.RetailerFeatures.Handlers.CommandHandlers
{
    public class UpdateRetailerCommand : IRequest<ApiResponse<string>>
    {
        public Guid Id { get; set; }
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
        public string? UpdatedBy { get; set; }


    }
    public class UpdateRetailerHandler : IRequestHandler<UpdateRetailerCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public UpdateRetailerHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }
        public async Task<ApiResponse<string>> Handle(UpdateRetailerCommand request, CancellationToken cancellationToken)
        {
            // Initialize response object
            var response = new ApiResponse<string>();

            // Retrieve the delivery address by id
            var retailer = await _unitOfWorkDb.retailerQueryRepository.GetByIdAsync(request.Id);

            // Check if the delivery address exists
            if (retailer == null)
            {
                throw new NotFoundException($"retailer  with id = {request.Id} not found");
            }


            try
            {


                // Update delivery address properties
                retailer.UpdatedBy = request.UpdatedBy;
                retailer.BIN = request.BIN;
                retailer.Name = request.Name;
                retailer.Contactperson = request.Contactperson;
                retailer.ContactPerNum = request.ContactPerNum;
                retailer.ContactNumber = request.ContactNumber;





                // Perform the update operation
                await _unitOfWorkDb.retailerCommandRepository.UpdateAsync(retailer);
                await _unitOfWorkDb.SaveAsync();

                // Set success response
                response.Success = true;
                response.Data = $"retailer with id = {retailer.Id} updated successfully";
                response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                response.Success = false;
                response.Data = "An error occurred while updating the retailer ";
                response.ErrorMessage = ex.Message;
                response.Status = HttpStatusCode.InternalServerError; // Set status code to 500 (Internal Server Error)
            }

            // Return the response
            return response;
        }
    }
}
