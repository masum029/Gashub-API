using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Project.Domail.Entities.Base;

namespace Project.Domail.Entities
{
    public class PurchaseDetail : BaseEntity
    {
        [Required(ErrorMessage = "Purchase ID is required.")]
        public Guid PurchaseID { get; set; }

        [ForeignKey("PurchaseID")]
        public Purchase Purchase { get; set; }

        [Required(ErrorMessage = "Product ID is required.")]
        public Guid ProductID { get; set; }

        [ForeignKey("ProductID")]
        public Product Product { get; set; }

        public int Quantity { get; set; }

        [Precision(18, 2)]
        public decimal UnitPrice { get; set; }

        [Precision(18, 2)]
        public decimal Discount { get; set; }

        [Precision(18, 2)]
        public decimal TotalPrice => Quantity * UnitPrice - Discount;
    }
}
