using MediatR;
using Project.Application.DTOs;
using Project.Application.Interfaces;

namespace Project.Application.Features.UserFeatures.Queries
{
    public class GetUserDetailsByUserNameQuery : IRequest<UserDetailsDTO>
    {
        public string UserName { get; set; }
    }

    public class GetUserDetailsByUserNameQueryHandler : IRequestHandler<GetUserDetailsByUserNameQuery, UserDetailsDTO>
    {
        private readonly IIdentityService _identityService;

        public GetUserDetailsByUserNameQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<UserDetailsDTO> Handle(GetUserDetailsByUserNameQuery request, CancellationToken cancellationToken)
        {
            var user = await _identityService.GetUserDetailsByUserNameAsync(request.UserName);
            return new UserDetailsDTO() { Id = user.userId, FirstName = user.FirstName, UserName = user.UserName, Email = user.email, Roles = user.roles };
        }
    }
}
