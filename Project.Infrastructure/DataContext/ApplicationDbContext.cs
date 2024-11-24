using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Domail.Entities;
using Project.Infrastructure.Identity;

namespace Project.Infrastructure.DataContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Define DbSets
        public DbSet<ProductSize>? ProductSizes { get; set; }
        public DbSet<Retailer>? Retailers { get; set; }
        public DbSet<Company>? Companies { get; set; }
        public DbSet<DeliveryAddress>? DeliveryAddresses { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<ProdReturn>? ProdReturns { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Stock>? Stocks { get; set; }
        public DbSet<Trader>? Traders { get; set; }
        public DbSet<Valve>? Valves { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public DbSet<ProductDiscunt>? ProductDiscounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureEntities(modelBuilder);
            ConfigureRelationships(modelBuilder);
            ConfigureDecimalPrecision(modelBuilder);
        }

        private void ConfigureEntities(ModelBuilder modelBuilder)
        {
            // Define any entity-specific configurations here
            modelBuilder.Entity<IdentityUserLogin<string>>();
        }

        private void ConfigureRelationships(ModelBuilder modelBuilder)
        {
            // Stock-Trader relationship
            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Trader)
                .WithMany()
                .HasForeignKey(s => s.TraderId)
                .OnDelete(DeleteBehavior.Restrict);

            // OrderDetail relationships
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany()
                .HasForeignKey(od => od.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductID)
                .OnDelete(DeleteBehavior.NoAction);

            // PurchaseDetail relationships
            modelBuilder.Entity<PurchaseDetail>()
                .HasOne(pd => pd.Product)
                .WithMany()
                .HasForeignKey(pd => pd.ProductID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PurchaseDetail>()
                .HasOne(pd => pd.Purchase)
                .WithMany(p => p.PurchaseDetails)
                .HasForeignKey(pd => pd.PurchaseID)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigureDecimalPrecision(ModelBuilder modelBuilder)
        {
            // ProductDiscount discounted price precision
            modelBuilder.Entity<ProductDiscunt>()
                .Property(pd => pd.DiscountedPrice)
                .HasColumnType("decimal(18, 2)");

            // ProductSize size precision
            modelBuilder.Entity<ProductSize>()
                .Property(ps => ps.Size)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
