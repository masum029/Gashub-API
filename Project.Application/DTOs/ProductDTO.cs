using Project.Domail.Entities;

namespace Project.Application.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public Guid ProdSizeId { get; set; }
        public Guid ProdValveId { get; set; }
        public string ProdImage { get; set; }
        public int ProdPrice { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public virtual Company Company { get; set; }
        public virtual ProductSize Size { get; set; }
        public virtual Valve Valve { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ProdReturn> Returns { get; set; }
    }
}
