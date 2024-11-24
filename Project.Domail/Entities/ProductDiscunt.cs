using Project.Domail.Entities.Base;

namespace Project.Domail.Entities
{
    public class ProductDiscunt : BaseEntity
    {

        public Guid ? ProductId { get; set; }
        public decimal DiscountedPrice { get; set; }
        public bool IsActive { get; set; }
        public DateTime ValidTill { get; set; }
        public Product ? Product { get; set; }
    }
}
