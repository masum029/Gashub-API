using Project.Domail.Abstractions.CommandRepositories;
using Project.Domail.Entities;
using Project.Infrastructure.DataContext;
using Project.Infrastructure.Implementation.Command.Base;


namespace Project.Infrastructure.Implementation.Command
{
    public class ValveCommandRepository : CommandRepository<Valve>, IValveCommandRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ValveCommandRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        // Implement additional methods specific to ValveCommandRepository here
    }
}
