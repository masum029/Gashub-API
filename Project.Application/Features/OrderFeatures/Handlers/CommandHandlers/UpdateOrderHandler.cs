using AutoMapper;
using MediatR;
using Project.Application.ApiResponse;
using Project.Application.Exceptions;
using Project.Domail.Abstractions;
using System.Net;

namespace Project.Application.Features.OrderFeatures.Handlers.CommandHandlers
{
    public class UpdateOrderCommand : IRequest<ApiResponse<string>>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public Guid ReturnProductId { get; set; }
        public string TransactionNumber { get; set; } 
        public string Comments { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsHold { get; set; }
        public bool IsCancel { get; set; }
        public bool IsPlaced { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsDispatched { get; set; }
        public bool IsReadyToDispatch { get; set; }
        public bool IsDelivered { get; set; }
    }
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        
        public UpdateOrderHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }

        public async Task<ApiResponse<string>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            // Initialize response object
            var response = new ApiResponse<string>();

            // Retrieve the delivery address by id
            var order = await _unitOfWorkDb.orderQueryRepository.GetByIdAsync(request.Id);

            // Check if the delivery address exists
            if (order == null)
            {
                throw new NotFoundException($"order with id = {request.Id} not found");
            }


            try
            {


                // Update delivery address properties
                order.UserId = request.UserId;
                order.ProductId=request.ProductId;
                order.ReturnProductId = request.ReturnProductId;
                order.UpdatedBy = request.UpdatedBy;
                order.Comments = request.Comments;
                order.TransactionNumber = request.TransactionNumber;
                order.IsDelivered = request.IsDelivered;
                order.IsPlaced = request.IsPlaced;
                order.IsHold = request.IsHold;
                order.IsConfirmed = request.IsConfirmed;
                order.IsReadyToDispatch = request.IsReadyToDispatch;
                order.IsDispatched = request.IsDispatched;
                order.IsCancel = request.IsCancel;




                if (order.IsDelivered)
                {
                    var UpdateORder = await _unitOfWorkDb.orderCommandRepository.UpdateFinalOrder(order);
                }



                // Perform the update operation
                await _unitOfWorkDb.orderCommandRepository.UpdateAsync(order);
                await _unitOfWorkDb.SaveAsync();

                // Set success response
                response.Success = true;
                response.Data = $"order with id = {order.Id} updated successfully";
                response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                response.Success = false;
                response.Data = "An error occurred while updating the order";
                response.ErrorMessage = ex.Message;
                response.Status = HttpStatusCode.InternalServerError; // Set status code to 500 (Internal Server Error)
            }

            // Return the response
            return response;
        }
    }
    
}
