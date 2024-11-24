using Project.Domail.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;


namespace Project.Domail.Entities
{
    public class Trader : BaseEntity
    {
        [ForeignKey("Company")]
        public Guid CompanyId { get; set; }
        public string  Name { get; set; }
        public string Contactperson { get; set; }
        public string ContactPerNum { get; set; }
        public string ContactNumber { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DeactivatedDate { get; set; }
        public string? DeactiveBy { get; set; }
        public string? BIN { get; set; }

        // Navigation properties
        public virtual ICollection<Stock>? Stocks { get; set; }
        public virtual Company ?Company { get; set; }

    }
}
