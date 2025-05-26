using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orcafin.Domain.Entities
{
    public class Employee : User
    {
        public string Role { get; set; }
    }
}

