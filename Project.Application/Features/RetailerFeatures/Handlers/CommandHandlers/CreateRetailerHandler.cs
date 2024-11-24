using AutoMapper;
using MediatR;
using Project.Application.ApiResponse;
using Project.Domail.Abstractions;
using Project.Domail.Entities;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Project.Application.Features.RetailerFeatures.Handlers.CommandHandlers
{
    public class CreateRetailerCommand : IRequest<ApiResponse<string>>
    {

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
    public class CreateRetailerHandler : IRequestHandler<CreateRetailerCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;

        public CreateRetailerHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }
        public async Task<ApiResponse<string>> Handle(CreateRetailerCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();

            try
            {
                var newRetailer = new Retailer
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now.Date,
                    CreatedBy = request.CreatedBy,
                    Name = request.Name,
                    Contactperson = request.Contactperson,
                    ContactPerNum = request.ContactPerNum,
                    ContactNumber = request.ContactNumber,
                    IsActive = true,
                    BIN = request.BIN,
                };

                await _unitOfWorkDb.retailerCommandRepository.AddAsync(newRetailer);
                await _unitOfWorkDb.SaveAsync();

                response.Success = true;
                response.Data = $" Retailer  id = {newRetailer.Id} created successfully!";
                response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = "An error occurred while creating the Retailer ";
                response.ErrorMessage = ex.Message;
                response.Status = HttpStatusCode.InternalServerError; // Set status code to 500 (Internal Server Error)
            }

            return response;
        }
    }
}
