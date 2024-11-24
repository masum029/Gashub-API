using Project.Domail.Abstractions.QueryRepositories;
using Project.Domail.Entities;
using Project.Infrastructure.DataContext;
using Project.Infrastructure.Implementation.Query.Base;


namespace Project.Infrastructure.Implementation.Query
{
    public class TraderQueryRepository : QueryRepository<Trader>, ITraderQueryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TraderQueryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        // Implement additional methods specific to TraderQueryRepository here
    }
}
