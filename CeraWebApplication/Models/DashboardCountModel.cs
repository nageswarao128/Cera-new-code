using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeraWebApplication.Models
{
    public class DashboardCountModel
    {
        public int resources { get; set; }
        public int policies { get; set; }
        public int users { get; set; }
        public decimal cost { get; set; }
    }
}
