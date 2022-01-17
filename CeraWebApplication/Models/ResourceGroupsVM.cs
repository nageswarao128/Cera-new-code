using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeraWebApplication.Models
{
    public class ResourceGroupsVM
    {
        public string Name { get; set; }
        public string RegionName { get; set; }
        public string SubscriptionId { get; set; }
        public string CloudProvider { get; set; }
        public decimal? cost { get; set; }
        public string currency { get; set; }
    }
}
