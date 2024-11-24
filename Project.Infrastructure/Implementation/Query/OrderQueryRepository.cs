using Project.Domail.Abstractions.QueryRepositories;
using Project.Domail.Entities;
using Project.Infrastructure.DataContext;
using Project.Infrastructure.Implementation.Query.Base;

namespace Project.Infrastructure.Implementation.Query
{
    public class OrderQueryRepository : QueryRepository<Order>, IOrderQueryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public OrderQueryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        // Implement additional methods specific to OrderQueryRepository here
    }
}
