using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Domail.Abstractions;
using Project.Domail.Abstractions.CommandRepositories;
using Project.Domail.Entities;
using Project.Infrastructure.DataContext;
using Project.Infrastructure.Implementation.Command.Base;


namespace Project.Infrastructure.Implementation.Command
{
    public class OrderCommandRepository : CommandRepository<Order>, IOrderCommandRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public OrderCommandRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> ConfirmOrder(Guid userId, Dictionary<string, int> itemQuantities)
        {
            if (userId == Guid.Empty || itemQuantities == null || !itemQuantities.Any())
                throw new ArgumentException("Invalid arguments for order confirmation.");

            await using var transaction = await _applicationDbContext.Database.BeginTransactionAsync();
            try
            {
                var returnProducts = new List<ProdReturn>();
                var orders = new List<Order>();

                // Assuming itemQuantities is of type Dictionary<Guid, int>
                foreach (var kvp in itemQuantities)
                {
                    var productId = Guid.Parse(kvp.Key) ; // This is the Guid key
                    var quantity = kvp.Value; // This is the int value

                    var product = await _applicationDbContext.Products.FindAsync(productId);
                    if (product == null)
                    {
                        throw new InvalidOperationException($"Product with ID {productId} does not exist.");
                    }

                    var returnProduct = new ProdReturn
                    {
                        ProductId = product.Id,
                        Name = product.Name,
                        ProdSizeId = product.ProdSizeId,
                        ProdValveId = product.ProdValveId,
                        IsConfirmedOrder = false
                    };

                    await _applicationDbContext.ProdReturns.AddAsync(returnProduct);
                    returnProducts.Add(returnProduct);
                }


                await _applicationDbContext.SaveChangesAsync();

                var transactionNumber = GenerateTransactionNumber();

                foreach (var returnProduct in returnProducts)
                {
                    // Try to get the quantity for the current product ID
                    if (itemQuantities.TryGetValue(returnProduct.ProductId.ToString(), out int quantity))
                    {
                        var order = new Order
                        {
                            UserId = userId, // UserId is now a Guid
                            ProductId = returnProduct.ProductId,
                            ReturnProductId = returnProduct.Id,
                            TransactionNumber = transactionNumber,
                            Comments = quantity.ToString() ,
                            IsHold = false,
                            IsCancel = false,
                            IsDelivered = false,
                            IsConfirmed = false,
                            IsPlaced = true,
                            IsDispatched = false,
                            IsReadyToDispatch = false,
                        };

                        orders.Add(order);
                    }
                    else
                    {
                        // Handle the case where the ProductId is not in the itemQuantities dictionary
                        Console.Error.WriteLine($"Product ID {returnProduct.ProductId} not found in itemQuantities.");
                        // You can decide whether to continue or throw an exception based on your business logic
                        throw new InvalidOperationException($"Quantity for Product ID {returnProduct.ProductId} is missing.");
                    }
                }


                await _applicationDbContext.Orders.AddRangeAsync(orders);

                await _applicationDbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.Error.WriteLine($"Transaction failed: {ex.Message}");
                return false;
            }
        }

        private static string GenerateTransactionNumber()
        {
            var now = DateTime.UtcNow;
            return $"{now:yyMMddHHmmssfff}{GenerateRandomString(5)}";
        }

        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<bool> UpdateFinalOrder(Order item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Order item cannot be null.");
            }

            // Begin a new database transaction
            await using var transaction = await _applicationDbContext.Database.BeginTransactionAsync();
            try
            {
                // Retrieve the associated return product by the ReturnProductId from the order
                var returnProduct = await _applicationDbContext.ProdReturns.FindAsync(item.ReturnProductId);

                if (returnProduct == null)
                {
                    throw new InvalidOperationException($"Return product with ID {item.ReturnProductId} does not exist.");
                }

                // Update the IsConfirmedOrder status
                returnProduct.IsConfirmedOrder = true;

                // Update the return product in the repository
                _applicationDbContext.ProdReturns.Update(returnProduct);

                // Save the changes to the return product
                await _applicationDbContext.SaveChangesAsync();

                // Retrieve the existing stock for the product
                var existingStock = await _applicationDbContext.Stocks
                    .FirstOrDefaultAsync(st => st.ProductId == item.ProductId);

                if (existingStock == null)
                {
                    throw new InvalidOperationException($"Stock for Product ID {item.ProductId} does not exist.");
                }
                if (!int.TryParse(item.Comments, out int quantity))
                {
                    throw new InvalidOperationException($"Invalid quantity value: {item.Comments}");
                }
                // Update the stock quantity
                existingStock.Quantity -= quantity;

                // Update the stock in the repository
                _applicationDbContext.Stocks.Update(existingStock);

                // Save the changes to the stock
                await _applicationDbContext.SaveChangesAsync();

                // Commit the transaction
                await transaction.CommitAsync();

                return true; // Indicate that the update was successful
            }
            catch (Exception ex)
            {
                // Rollback the transaction on error
                await transaction.RollbackAsync();
                Console.Error.WriteLine($"Transaction failed: {ex.Message}");
                return false; // Indicate that the update failed
            }
        }


    }
}
