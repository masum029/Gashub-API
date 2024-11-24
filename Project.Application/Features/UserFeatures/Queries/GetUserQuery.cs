using MediatR;
using Project.Application.DTOs;
using Project.Application.Interfaces;


namespace Project.Application.Features.UserFeatures.Queries
{
    public class GetUserQuery : IRequest<List<UserDTO>>
    {
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, List<UserDTO>>
    {
        private readonly IIdentityService _identityService;

        public GetUserQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<List<UserDTO>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _identityService.GetAllUsersAsync();
            return users.Select(x => new UserDTO()
            {
                Id = x.id,
                UserName = x.userName,
                Email = x.email,
                FirstName=x.FirstName,
                LastName=x.LastName,
            }).ToList();
        }
    }
}
