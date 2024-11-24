using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs
{
    public class ConfirmOrderDTOs
    {
        public string UserID { get; set; }
        public Dictionary<string, int> ProductIdAndQuentity { get; set; }
    }
}
