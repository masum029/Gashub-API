
using Microsoft.EntityFrameworkCore;


namespace InventoryApi.DTOs
{
    public class PurchaseDTOs 
    {
        public Guid Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Guid CompanyId { get; set; }
        public decimal TotalAmount { get; set; }
 
    }
}
