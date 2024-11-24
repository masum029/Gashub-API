
using Project.Domail.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Domail.Entities
{
    public class Stock : BaseEntity
    {
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        [ForeignKey("Trader")]
        public Guid TraderId { get; set; }

        public int Quantity { get; set; }
        public bool IsQC { get; set; }
        public bool IsActive { get; set; }

        // Navigation properties
        public virtual Product? Product { get; set; }
        public virtual Trader? Trader { get; set; }

    }
}
