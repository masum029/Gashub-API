using MediatR;
using Project.Application.ApiResponse;
using Project.Application.Interfaces;
using System.Net;

namespace Project.Application.Features.RoleFeatures.Commands
{
    public class RoleCreateCommand : IRequest<ApiResponse<string>>
    {
        public string RoleName { get; set; }
    }

    public class RoleCreateCommandHandler : IRequestHandler<RoleCreateCommand, ApiResponse<string>>
    {
        private readonly IIdentityService _identityService;

        public RoleCreateCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<ApiResponse<string>> Handle(RoleCreateCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();
            try
            {
                var result = await _identityService.CreateRoleAsync(request.RoleName);
                if (result)
                {
                    response.Success = true;
                    response.Data = $" Role created successfully!";
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
