using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeraWebApplication.Models
{
    public class ResourceTypeCount
    {
        public string ResourceType { get; set; }
        public int ResourceCount { get; set; }
        public string Color { get; set; }
        public decimal ConsumedPercent { get; set; }
    }
}
