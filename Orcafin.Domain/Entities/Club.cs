using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orcafin.Domain.Entities
{
    public class Club
    {
        public int Id { get; set; }
        public DateTime JoinDate { get; set; }
        public string? Benefits { get; set; }
    }
}

