using Project.Domail.Entities;

namespace Project.Application.DTOs
{
    public class CompanyDTO
    {
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; private set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Contactperson { get; set; }
        public string ContactPerNum { get; set; }
        public string ContactNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime DeactivatedDate { get; set; }
        public string DeactiveBy { get; set; }
        public string BIN { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Trader> Traders { get; set; }



    }
}
