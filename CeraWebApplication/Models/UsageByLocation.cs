using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeraWebApplication.Models
{
    public class UsageByLocation
    {
        public string instanceLocation { get; set; }
        public decimal ConsumedCost { get; set; }
    }
}
