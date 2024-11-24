using Project.Domail.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs
{
    public class TraderDTO
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public string Contactperson { get; set; }
        public string ContactPerNum { get; set; }
        public string ContactNumber { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DeactivatedDate { get; set; }
        public string DeactiveBy { get; set; }
        public string BIN { get; set; }
        public virtual ICollection<Stock>? Stocks { get; set; }
        public virtual Company? Company { get; set; }

    }
}
