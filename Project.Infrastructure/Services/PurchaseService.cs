using AutoMapper;
using InventoryApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Application.DTOs;
using Project.Application.Exceptions;
using Project.Application.Interfaces;
using Project.Domail.Abstractions;
using Project.Domail.Entities;
using Project.Infrastructure.DataContext;
using Project.Infrastructure.Identity;

namespace Project.Infrastructure.Services
{
    public class PurchaseService : IBaseServices<PurchaseDTOs>, IPurchaseServices
    {
        private readonly IUnitOfWorkDb _unitOfWorkRepository;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        public PurchaseService(IUnitOfWorkDb unitOfWorkRepository, IMapper mapper, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<bool> CreateAsync(PurchaseDTOs entity)
        {
            var newBranch = new Purchase
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now, 
                PurchaseDate = DateTime.Now,
                CompanyId = entity.CompanyId,
                TotalAmount = entity.TotalAmount,
            };
            await _unitOfWorkRepository.purchaseCommandRepository.AddAsync(newBranch);
            await _unitOfWorkRepository.SaveAsync();
            return true;
        }
        public async Task<bool> UpdateAsync(Guid id, PurchaseDTOs entity)
        {
            var item = await _unitOfWorkRepository.purchaseQueryRepository.GetByIdAsync(id);
            if (item == null || item?.Id != id)
            {
                throw new NotFoundException($" Purchase  with id = {id} not found");
            }


            // Update properties with validation
            // item.CartID = string.IsNullOrWhiteSpace(entity.CartID) ? item.CartID : entity.CartID.Trim();


            item.CompanyId = entity.CompanyId;
            item.TotalAmount = entity.TotalAmount;
            // Perform update operation
            await _unitOfWorkRepository.purchaseCommandRepository.UpdateAsync(item);
            await _unitOfWorkRepository.SaveAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var deleteItem = await _unitOfWorkRepository.purchaseQueryRepository.GetByIdAsync(id);

            if (deleteItem == null)
            {
                throw new NotFoundException($"Purchase  with id = {id} not found");
            }
            await _unitOfWorkRepository.purchaseCommandRepository.DeleteAsync(deleteItem);
            await _unitOfWorkRepository.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<PurchaseDTOs>> GetAllAsync()
        {
            // Retrieve the list of purchases from the repository
            var itemList = await _unitOfWorkRepository.purchaseQueryRepository.GetAllAsync();

            // Map each purchase item to PurchaseDTOs
            var result = itemList.Select(purchase => new PurchaseDTOs
            {
                Id=purchase.Id,
                PurchaseDate = purchase.PurchaseDate,
                CompanyId = (Guid)purchase?.CompanyId,
                TotalAmount = purchase.TotalAmount
            });

            return result;
        }


        public async Task<PurchaseDTOs> GetByIdAsync(Guid id)
        {
            var item = await _unitOfWorkRepository.purchaseQueryRepository.GetByIdAsync(id);
            if (item == null || item?.Id != id)
            {
                throw new NotFoundException($"Purchase with id = {id} not found");
            }
            var result = _mapper.Map<PurchaseDTOs>(item);
            return result;
        }

        public async Task<bool> PurchaseProduct(PurchaseItemDTOs entitys)
        {
            // Validate the input data
            if (entitys == null || entitys.Products == null || !entitys.Products.Any())
            {
                throw new ValidationException("Invalid purchase item data.");
            }

            // Start a new database transaction
            await using var transaction = await _context.Database.BeginTransactionAsync();
            var totalAmount = 0m; // Decimal initialization for totalAmount

            try
            {
                // Retrieve the logged-in user's UserId claim from JWT
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid parsedUserId))
                {
                    throw new ValidationException("User ID not found or invalid in JWT claims.");
                }

                // Retrieve the user details using UserManager
                var user = await _userManager.FindByIdAsync(parsedUserId.ToString());
                if (user == null || user.TraderId == null)
                {
                    throw new NotFoundException("User or Trader ID not found.");
                }

                var traderId = user.TraderId.Value;

                // Create the new purchase record
                var newPurchase = new Purchase
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.UtcNow,
                    PurchaseDate = DateTime.UtcNow,
                    CompanyId = entitys.CompanyId,
                    TotalAmount = 0 // Set the initial total amount to 0
                };

                await _context.Purchases.AddAsync(newPurchase);

                // Initialize a list to hold the purchase details
                var purchaseDetails = new List<PurchaseDetail>();

                // Process each product in the purchase
                foreach (var productDto in entitys.Products)
                {
                    // Calculate the amount for the current product
                    var productTotal = (productDto.Quantity * productDto.Price) - productDto.Discount;
                    totalAmount += productTotal;

                    var newPurchaseDetail = new PurchaseDetail
                    {
                        Id = Guid.NewGuid(),
                        CreationDate = DateTime.UtcNow,
                        PurchaseID = newPurchase.Id,
                        ProductID = productDto.ProductID,
                        Quantity = productDto.Quantity,
                        UnitPrice = productDto.Price,
                        Discount = productDto.Discount,
                    };

                    purchaseDetails.Add(newPurchaseDetail);
                }

                await _context.PurchaseDetails.AddRangeAsync(purchaseDetails);
                newPurchase.TotalAmount = totalAmount;

                // Update the stock for each product
                foreach (var productDto in entitys.Products)
                {
                    var product = await _context.Products.FindAsync(productDto.ProductID);
                    if (product != null)
                    {
                        var existingStock = await _context.Stocks.FirstOrDefaultAsync(s => s.ProductId == product.Id);

                        if (existingStock != null)
                        {
                            existingStock.Quantity += productDto.Quantity;
                            _context.Stocks.Update(existingStock);
                        }
                        else
                        {
                            var newStock = new Stock
                            {
                                Id = Guid.NewGuid(),
                                ProductId = product.Id,
                                TraderId = traderId,
                                Quantity = productDto.Quantity,
                                IsQC = true,
                                IsActive = true,
                                CreationDate = DateTime.UtcNow,
                                CreatedBy = "System"
                            };

                            await _context.Stocks.AddAsync(newStock);
                        }
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }




    }
}
