using MediatR;
using Project.Application.ApiResponse;
using Project.Application.Exceptions;
using Project.Domail.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Project.Application.Features.CompanyFeatures.Handlers.CommandHandlers
{

    public class UpdateCompanyCommand : IRequest<ApiResponse<string>>
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
    public class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        public UpdateCompanyHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
           
        }

        public async Task<ApiResponse<string>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();

            var company = await _unitOfWorkDb.companyrQueryRepository.GetByIdAsync(request.Id);

            if (company == null || company.Id != request.Id)
            {
                throw new NotFoundException($"Company with id = {request.Id} not found");
            }

            try
            {
                // Update company properties
                company.BIN = request.BIN;
                company.Name = request.Name;
                company.Contactperson = request.Contactperson;
                company.ContactPerNum = request.ContactPerNum;
                company.ContactNumber = request.ContactNumber;
                company.UpdatedBy = request.UpdatedBy;

                // Perform update operation
                await _unitOfWorkDb.companyCommandRepository.UpdateAsync(company);
                await _unitOfWorkDb.SaveAsync();

                // Map the updated company to your DTO model if needed
                response.Success = true;
                response.Data = $"Company with id = {company.Id} updated successfully";
                response.Status = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                response.Success = false;
                response.Data = "An error occurred while updating the company";
                response.ErrorMessage = ex.Message;
                response.Status = HttpStatusCode.InternalServerError;
            }

            return response;
        }

    }
}
//          