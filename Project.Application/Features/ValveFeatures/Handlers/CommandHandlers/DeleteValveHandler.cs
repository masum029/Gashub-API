using MediatR;
using Project.Application.ApiResponse;
using Project.Application.Exceptions;
using Project.Domail.Abstractions;
using System.Net;


namespace Project.Application.Features.ValveFeatures.Handlers.CommandHandlers
{
    public class DeleteValveCommand : IRequest<ApiResponse<string>>
    {
        public DeleteValveCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
    public class DeleteValveHandler : IRequestHandler<DeleteValveCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public DeleteValveHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }
        public async Task<ApiResponse<string>> Handle(DeleteValveCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();
            var valve = await _unitOfWorkDb.valverQueryRepository.GetByIdAsync(request.Id);

            if (valve == null)
            {
                throw new NotFoundException($"valve with id = {request.Id} not found");
            }
            else
            {
                try
                {
                    await _unitOfWorkDb.valveCommandRepository.DeleteAsync(valve);
                    await _unitOfWorkDb.SaveAsync();

                    response.Success = true;
                    response.Data = $"valve with id = {valve.Id} deleted successfully";
                    response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Data = "An error occurred while deleting the valve  ";
                    response.ErrorMessage = ex.Message;
                    response.Status = HttpStatusCode.InternalServerError;  // Set status code to 500 (Internal Server Error)
                }

            }
            return response;
        }
    }
}
