using MediatR;


namespace Project.Application.Features.UserFeatures.Commands
{
    public class UpdateUserRolesCommand : IRequest<int>
    {
        public string userName { get; set; }
        public IList<string> Roles { get; set; }
    }
    public class UpdateUserRolesCommandHandler : IRequestHandler<UpdateUserRolesCommand, int>
    {
        public Task<int> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
