using MediatR;
using Project.Application.ApiResponse;
using Project.Domail.Abstractions;
using Project.Domail.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.OrderFeatures.Handlers.CommandHandlers
{
    public class CreateConfirmOrderComment : IRequest<ApiResponse<string>>
    {
        public string UserID { get; set; }
        public Dictionary<string, int> ProductIdAndQuentity { get; set; }
    }
    public class CreateConfirmOrderHandler : IRequestHandler<CreateConfirmOrderComment, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;


        public CreateConfirmOrderHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;

        }


        public async Task<ApiResponse<string>> Handle(CreateConfirmOrderComment request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();
            var userId = Guid.Parse(request.UserID);

            var result = await _unitOfWorkDb.orderCommandRepository.ConfirmOrder(userId, request.ProductIdAndQuentity);
            response.Success = true;
            response.Data = $"Order Confirmed successfully!";
            response.Status = HttpStatusCode.OK; // 200 OK
            return response;
        }

    }
}
