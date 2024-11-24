﻿using Project.Domail.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Domail.Entities
{
    public class Product : BaseEntity
    {
        [ForeignKey("Company")]
        public Guid CompanyId { get; set; }
        public string? Name { get; set; }
        [ForeignKey("Size")]
        public Guid ProdSizeId { get; set; }
        [ForeignKey("Valve")]
        public Guid ProdValveId { get; set; }
        public string? ProdImage { get; set; }
        public int  ProdPrice { get; set; }  
        public bool IsActive { get; set; }

        // Navigation properties
        public virtual Company Company { get; set; }
        public virtual ProductSize Size { get; set; }
        public virtual Valve Valve { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ProdReturn> Returns { get; set; }

    }
}
