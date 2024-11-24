using Project.Domail.Abstractions.QueryRepositories;
using Project.Domail.Entities;
using Project.Infrastructure.DataContext;
using Project.Infrastructure.Implementation.Query.Base;


namespace Project.Infrastructure.Implementation.Query
{
    public class ValveQueryRepository : QueryRepository<Valve>, IValverQueryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ValveQueryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        // Implement additional methods specific to ValveQueryRepository here
    }
}
