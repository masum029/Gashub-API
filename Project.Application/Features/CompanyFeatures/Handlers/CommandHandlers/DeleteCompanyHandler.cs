using MediatR;
using Project.Application.ApiResponse;
using Project.Application.Exceptions;
using Project.Domail.Abstractions;
using System.Net;

namespace Project.Application.Features.CompanyFeatures.Handlers.CommandHandlers
{
    public class DeleteCompanyCommand : IRequest<ApiResponse<string>>
    {
        public DeleteCompanyCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
    public class DeleteCompanyHandler : IRequestHandler<DeleteCompanyCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public DeleteCompanyHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }

        public async Task<ApiResponse<string>> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();
            var deleteCompany = await _unitOfWorkDb.companyrQueryRepository.GetByIdAsync(request.Id);

            if (deleteCompany == null)
            {
                throw new NotFoundException($"Company with id = {request.Id} not found");
            }

            try
            {
                await _unitOfWorkDb.companyCommandRepository.DeleteAsync(deleteCompany);
                await _unitOfWorkDb.SaveAsync();

                response.Success = true;
                response.Data = $"Company with id = {deleteCompany.Id} deleted successfully";
                response.Status = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                response.Success = false;
                response.Data = "An error occurred while deleting the company";
                response.ErrorMessage = ex.Message;
                response.Status = HttpStatusCode.InternalServerError;
            }

            return response;
        }

    }
}
