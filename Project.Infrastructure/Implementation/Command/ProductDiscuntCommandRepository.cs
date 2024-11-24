using Project.Domail.Abstractions.CommandRepositories;
using Project.Domail.Entities;
using Project.Infrastructure.DataContext;
using Project.Infrastructure.Implementation.Command.Base;

namespace Project.Infrastructure.Implementation.Command
{
    public class ProductDiscuntCommandRepository : CommandRepository<ProductDiscunt>, IProductDiscuntCommandRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductDiscuntCommandRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        // Implement additional methods specific to CustomerCommandRepository here


    }
}
