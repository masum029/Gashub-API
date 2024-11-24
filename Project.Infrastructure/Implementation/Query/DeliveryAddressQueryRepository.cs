using Project.Domail.Abstractions.QueryRepositories;
using Project.Domail.Entities;
using Project.Infrastructure.DataContext;
using Project.Infrastructure.Implementation.Query.Base;

namespace Project.Infrastructure.Implementation.Query
{
    public class DeliveryAddressQueryRepository : QueryRepository<DeliveryAddress>, IDeliveryAddressQueryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DeliveryAddressQueryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        // Implement additional methods specific to DeliveryAddressQueryRepository here
    }
}
