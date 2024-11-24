using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs
{
    public class PurchaseItemDTOs
    {
        public Guid CompanyId { get; set; }
        public List<PurchaseProductItemDTOs> Products { get; set; }
    }
    public class PurchaseProductItemDTOs
    {
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    }
}
