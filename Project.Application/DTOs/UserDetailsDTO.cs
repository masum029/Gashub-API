using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs
{
    public class UserDetailsDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
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
        public IList<string> Roles { get; set; }
    }
}
