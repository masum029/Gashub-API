using Project.Domail.Abstractions.CommandRepositories;
using Project.Domail.Entities;
using Project.Infrastructure.DataContext;
using Project.Infrastructure.Implementation.Command.Base;


namespace Project.Infrastructure.Implementation.Command
{
    public class TraderCommandRepository : CommandRepository<Trader>, ITraderCommandRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TraderCommandRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        // Implement additional methods specific to TraderCommandRepository here
    }
}
