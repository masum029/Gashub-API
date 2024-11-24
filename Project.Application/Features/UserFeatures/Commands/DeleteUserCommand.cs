using MediatR;
using Project.Application.ApiResponse;
using Project.Application.Interfaces;
using System.Net;


namespace Project.Application.Features.UserFeatures.Commands
{
    public class DeleteUserCommand : IRequest<ApiResponse<string>>
    {
        public string Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApiResponse<string>>
    {
        private readonly IIdentityService _identityService;

        public DeleteUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<ApiResponse<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();
            try
            {
                var result = await _identityService.DeleteUserAsync(request.Id);
                if (result)
                {
                    response.Success = true;
                    response.Data = $"User Delete Successfully!";
                    response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = "Server Error";
                response.ErrorMessage = ex.Message;
                response.Status = HttpStatusCode.InternalServerError; // Set status code to 500 (Internal Server Error)
            }
            return response;
        }
    }
}
