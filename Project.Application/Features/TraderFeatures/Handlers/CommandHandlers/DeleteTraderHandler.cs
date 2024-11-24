using MediatR;
using Project.Application.ApiResponse;
using Project.Application.Exceptions;
using Project.Domail.Abstractions;
using System.Net;


namespace Project.Application.Features.TraderFeatures.Handlers.CommandHandlers
{
    public class DeleteTraderCommand : IRequest<ApiResponse<string>>
    {
        public DeleteTraderCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
    public class DeleteTraderHandler : IRequestHandler<DeleteTraderCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public DeleteTraderHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }

        public async Task<ApiResponse<string>> Handle(DeleteTraderCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();
            var trader = await _unitOfWorkDb.traderQueryRepository.GetByIdAsync(request.Id);

            if (trader == null)
            {
                throw new NotFoundException($"trader with id = {request.Id} not found");
            }
            else
            {
                try
                {
                    await _unitOfWorkDb.traderCommandRepository.DeleteAsync(trader);
                    await _unitOfWorkDb.SaveAsync();

                    response.Success = true;
                    response.Data = $"trader with id = {trader.Id} deleted successfully";
                    response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Data = "An error occurred while deleting the trader  ";
                    response.ErrorMessage = ex.Message;
                    response.Status = HttpStatusCode.InternalServerError;  // Set status code to 500 (Internal Server Error)
                }

            }
            return response;
        }
    }
}
