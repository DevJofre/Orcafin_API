using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orcafin.Domain.Entities
{
    public class Customer : User
    {
        public bool IsClubMember { get; set; }
        public Club? Club { get; set; } 
    }
}

