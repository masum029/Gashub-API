using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs
{
    public class AuthDTO
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
