using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.UserFeatures.Commands
{
    public class EditUserProfileCommand : IRequest<int>
    {
        public EditUserProfileCommand(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
    public class EditUserProfileCommandHandler : IRequestHandler<EditUserProfileCommand, int>
    {
        public Task<int> Handle(EditUserProfileCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
