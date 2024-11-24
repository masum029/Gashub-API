using MediatR;
using Project.Application.ApiResponse;
using Project.Application.Interfaces;
using System.Net;

namespace Project.Application.Features.UserFeatures.Commands
{
    public class CreateUserCommand : IRequest<ApiResponse<string>>
    {
        public string FirstName { get; set; }
        public string LaststName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public Guid ?TraderId { get; set; }
        public string ConfirmationPassword { get; set; }
        public List<string> Roles { get; set; }
    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse<string>>
    {
        private readonly IIdentityService _identityService;

        public CreateUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<ApiResponse<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();
            try
            {
                if(request.Password == request.ConfirmationPassword)
                {
                    var trader = request.TraderId.HasValue ? request.TraderId.Value : Guid.Empty;

                    var result = await _identityService.CreateUserAsync(request.UserName, request.Password, request.Email, request.FirstName, request.LaststName, request.PhoneNumber, trader, request.Roles);
                    if (result.isSucceed)
                    {
                        response.Success = true;
                        response.Data = $"User id = {result.userId} Created Successfully!";
                        response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
                    }
                  
                }
                else
                {
                    response.Success = false;
                    response.Data = "Server Error";
                    response.ErrorMessage = "ConfirmationPassword : Password and confirmation password do not match";
                    response.Status = HttpStatusCode.InternalServerError; // Set status code to 500 (Internal Server Error)
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


