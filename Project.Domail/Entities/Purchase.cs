using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Project.Domail.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Project.Domail.Entities
{
    public class Purchase : BaseEntity
    {
        
        public DateTime PurchaseDate { get; set; }
        public Guid ? CompanyId { get; set; }
        public Company Company { get; set; }

        [Precision(18, 2)]
        public decimal TotalAmount { get; set; }

        public ICollection<PurchaseDetail> PurchaseDetails { get; set; }
    }
}
