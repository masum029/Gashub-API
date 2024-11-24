using Project.Domail.Abstractions.QueryRepositories;
using Project.Domail.Entities;
using Project.Infrastructure.DataContext;
using Project.Infrastructure.Implementation.Query.Base;

namespace Project.Infrastructure.Implementation.Query
{
    public class RetailerQueryRepository : QueryRepository<Retailer> , IRetailerQueryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RetailerQueryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        // Implement additional methods specific to RetailerQueryRepository here
    }
}
