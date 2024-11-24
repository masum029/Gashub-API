using MediatR;
using Project.Application.ApiResponse;
using Project.Application.Exceptions;
using Project.Domail.Abstractions;
using System.Net;

namespace Project.Application.Features.OrderFeatures.Handlers.CommandHandlers
{
    public class DeleteOrderCommand : IRequest<ApiResponse<string>>
    {
        public DeleteOrderCommand(Guid id)
        {
            this.id = id;
        }

        public Guid id { get; private set; }
    }
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public DeleteOrderHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }
   

        public async Task<ApiResponse<string>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();
            var order = await _unitOfWorkDb.orderQueryRepository.GetByIdAsync(request.id);

            if (order == null)
            {
                throw new NotFoundException($"order with id = {request.id} not found");
            }
            else
            {
                try
                {
                    await _unitOfWorkDb.orderCommandRepository.DeleteAsync(order);
                    await _unitOfWorkDb.SaveAsync();

                    response.Success = true;
                    response.Data = $"order with id = {order.Id} deleted successfully";
                    response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Data = "An error occurred while deleting the order ";
                    response.ErrorMessage = ex.Message;
                    response.Status = HttpStatusCode.InternalServerError;  // Set status code to 500 (Internal Server Error)
                }

            }
            return response;
        }
    }
}
