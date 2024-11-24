using MediatR;
using Project.Application.DTOs;
using Project.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.RoleFeatures.Queries
{
    public class GetRoleByIdQuery : IRequest<RoleDTO>
    {
        public string RoleId { get; set; }
    }

    public class GetRoleQueryByIdHandler : IRequestHandler<GetRoleByIdQuery, RoleDTO>
    {
        private readonly IIdentityService _identityService;

        public GetRoleQueryByIdHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<RoleDTO> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _identityService.GetRoleByIdAsync(request.RoleId);
            return new RoleDTO() { Id = role.id, RoleName = role.roleName };
        }
    }
}
