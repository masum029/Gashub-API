using Microsoft.AspNetCore.Identity;
using Project.Domail.Entities;


namespace Project.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; private set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UserImg { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DeactivatedDate { get; set; }
        public string? DeactiveBy { get; set; }
        public string? TIN { get; set; }
        public bool? IsBlocked { get; set; }
        public Guid ? TraderId { get; set; }
        // Navigation properties
        public virtual ICollection<DeliveryAddress>? DeliveryAddresses { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }

        public ApplicationUser()
        {
            UpdateDate = DateTime.Now;
        }
    }
}
