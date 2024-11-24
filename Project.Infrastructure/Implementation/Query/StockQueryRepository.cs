using Project.Domail.Abstractions.QueryRepositories;
using Project.Domail.Entities;
using Project.Infrastructure.DataContext;
using Project.Infrastructure.Implementation.Query.Base;


namespace Project.Infrastructure.Implementation.Query
{
    public class StockQueryRepository : QueryRepository<Stock>, IStockQueryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public StockQueryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        // Implement additional methods specific to StockQueryRepository here
    }
}
