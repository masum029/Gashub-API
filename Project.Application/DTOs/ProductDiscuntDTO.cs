using Project.Domail.Entities;


namespace Project.Application.DTOs
{
    public class ProductDiscuntDTO
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; private set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Guid ProductId { get; set; }
        public decimal DiscountedPrice { get; set; }
        public bool IsActive { get; set; }
        public DateTime ValidTill { get; set; }
        public Product Product { get; set; }
    }
}
