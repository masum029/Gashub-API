using MediatR;
using Project.Application.DTOs;
using Project.Application.Interfaces;


namespace Project.Application.Features.RoleFeatures.Queries
{
    public class GetRoleQuery : IRequest<IList<RoleDTO>>
    {

    }

    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, IList<RoleDTO>>
    {
        private readonly IIdentityService _identityService;

        public GetRoleQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<IList<RoleDTO>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var roles = await _identityService.GetRolesAsync();
            return roles.Select(role => new RoleDTO() { Id = role.id, RoleName = role.roleName }).ToList();
        }
    }
}
