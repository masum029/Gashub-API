using MediatR;
using Project.Application.ApiResponse;
using Project.Application.Exceptions;
using Project.Domail.Abstractions;
using System.Net;

namespace Project.Application.Features.DeliveryAddressFeatures.Handlers.CommandHandlers
{
    public class DeleteDeliveryAddressCommand : IRequest<ApiResponse<string>>
    {
        public DeleteDeliveryAddressCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
    public class DeleteDeliveryAddressHandler : IRequestHandler<DeleteDeliveryAddressCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public DeleteDeliveryAddressHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }
 
        public async Task<ApiResponse<string>> Handle(DeleteDeliveryAddressCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();
            var deliveryAddress = await _unitOfWorkDb.deliveryAddressQueryRepository.GetByIdAsync(request.Id);

            if (deliveryAddress == null)
            {
                throw new NotFoundException($"Delivery Address with id = {request.Id} not found");
            }
            else {
                try
                {
                    await _unitOfWorkDb.deliveryAddressCommandRepository.DeleteAsync(deliveryAddress);
                    await _unitOfWorkDb.SaveAsync();

                    response.Success = true;
                    response.Data = $"Delivery Address with id = {deliveryAddress.Id} deleted successfully";
                    response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Data = "An error occurred while deleting the delivery address";
                    response.ErrorMessage = ex.Message;
                    response.Status = HttpStatusCode.InternalServerError;  // Set status code to 500 (Internal Server Error)
                }

            }
            return response;
        }
    }
}
