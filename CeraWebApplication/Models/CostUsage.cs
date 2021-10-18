using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeraWebApplication.Models
{
    public class CostUsage
    {
        public string resourceType { get; set; }
        public decimal pretaxCost { get; set; }
    }
}
