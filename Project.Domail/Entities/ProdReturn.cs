using Project.Domail.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Domail.Entities
{
    public class ProdReturn : BaseEntity
    {
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public Guid ProdSizeId { get; set; }
        public Guid ProdValveId { get; set; }

        public bool IsConfirmedOrder { get; set; }
        // Navigation property
        public virtual Product Product { get; set; }

    }
}
