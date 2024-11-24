using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Exceptions
{
    public class Timeout_Exceptio : Exception
    {
        public Timeout_Exceptio(string message) : base(message) { }
    }
}
