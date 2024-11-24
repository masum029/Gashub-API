using Project.Domail.Abstractions;
using Project.Domail.Abstractions.CommandRepositories;
using Project.Domail.Abstractions.QueryRepositories;
using Project.Infrastructure.DataContext;
using Project.Infrastructure.Implementation.Command;
using Project.Infrastructure.Implementation.Query;

namespace Project.Infrastructure.Implementation
{
    public class UnitOfWorkDb : IUnitOfWorkDb
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IProductSizeCommandRepository productSizeCommandRepository { get; private set; }

        public IProductSizeQueryRepository productSizeQueryRepository { get; private set; }

        public IRetailerCommandRepository retailerCommandRepository { get; private set; }

        public IRetailerQueryRepository retailerQueryRepository { get; private set; }

        public ICompanyCommandRepository companyCommandRepository { get; private set; }

        public ICompanyrQueryRepository companyrQueryRepository { get; private set; }

        public IDeliveryAddressQueryRepository deliveryAddressQueryRepository { get; private set; }

        public IDeliveryAddressCommandRepository deliveryAddressCommandRepository { get; private set; }

        public IOrderCommandRepository orderCommandRepository { get; private set; }

        public IOrderQueryRepository orderQueryRepository { get; private set; }

        public IProdReturnCommandRepository prodReturnCommandRepository { get; private set; }

        public IProdReturnQueryRepository prodReturnQueryRepository { get; private set; }

        public IProductCommandRepository productCommandRepository { get; private set; }
        public IProductQueryRepository productQueryRepository { get; private set; }

        public IStockCommandRepository stockCommandRepository { get; private set; }

        public IStockQueryRepository stockQueryRepository { get; private set; }

        public ITraderCommandRepository traderCommandRepository { get; private set; }

        public ITraderQueryRepository traderQueryRepository { get; private set; }

        public IValveCommandRepository valveCommandRepository { get; private set; }

        public IValverQueryRepository valverQueryRepository { get; private set; }

        public IProductDiscuntCommandRepository productDiscuntCommandRepository { get; private set; }

        public IProductDiscuntQueryRepository productDiscuntQueryRepository { get; private set; }

        public IPurchaseCommandRepository purchaseCommandRepository { get; private set; }

        public IPurchaseQueryRepository purchaseQueryRepository { get; private set; }

        public UnitOfWorkDb(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            productSizeQueryRepository= new ProductSizeQueryRepository(applicationDbContext);
            productSizeCommandRepository = new ProductSizeCommandRepository(applicationDbContext);
            retailerCommandRepository = new RetailerCommandRepository(applicationDbContext);
            retailerQueryRepository = new RetailerQueryRepository(applicationDbContext);
            companyCommandRepository = new CompanyCommandRepository(applicationDbContext);
            companyrQueryRepository = new CompanyQueryRepository(applicationDbContext);
            deliveryAddressCommandRepository = new DeliveryAddressCommandRepository(applicationDbContext);
            deliveryAddressQueryRepository = new DeliveryAddressQueryRepository(applicationDbContext);
            orderCommandRepository = new OrderCommandRepository(applicationDbContext);
            orderQueryRepository = new OrderQueryRepository(applicationDbContext);
            prodReturnCommandRepository = new ProdReturnCommandRepository(applicationDbContext);
            prodReturnQueryRepository = new ProdReturnQueryRepository(applicationDbContext);
            productCommandRepository = new ProductCommandRepository(applicationDbContext);  
            productQueryRepository = new ProductQueryRepository(applicationDbContext);
            stockCommandRepository = new StockCommandRepository(applicationDbContext);
            stockQueryRepository = new StockQueryRepository(applicationDbContext);
            traderCommandRepository = new TraderCommandRepository(applicationDbContext);
            traderQueryRepository = new TraderQueryRepository(applicationDbContext);
            valveCommandRepository = new ValveCommandRepository(applicationDbContext);
            valverQueryRepository = new ValveQueryRepository(applicationDbContext);
            productDiscuntCommandRepository = new ProductDiscuntCommandRepository(applicationDbContext);
            productDiscuntQueryRepository = new ProductDiscuntQueryRepository(applicationDbContext);
            purchaseCommandRepository = new PurchaseCommandRepository(applicationDbContext);
            purchaseQueryRepository=new PurchaseQueryRepository(applicationDbContext);

        }



        public async Task SaveAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
