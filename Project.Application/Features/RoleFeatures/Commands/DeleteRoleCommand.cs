using MediatR;
using Project.Application.ApiResponse;
using Project.Application.Interfaces;
using System.Net;

namespace Project.Application.Features.RoleFeatures.Commands
{
    public class DeleteRoleCommand : IRequest<ApiResponse<string>>
    {
        public string RoleId { get; set; }
    }

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, ApiResponse<string>>
    {
        private readonly IIdentityService _identityService;

        public DeleteRoleCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<ApiResponse<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();
            try
            {
                var result = await _identityService.DeleteRoleAsync(request.RoleId);
                if (result)
                {
                    response.Success = true;
                    response.Data = $"role deleted Successfully!";
                    response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
                }
            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Data = "Server Error";
                response.ErrorMessage = ex.Message;
                response.Status = HttpStatusCode.InternalServerError;
            }
            return response;
        }
    }
}
