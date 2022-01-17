using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeraWebApplication.Models
{
    public class DashboardResources
    {
        public string Name { get; set; }
        public string ResourceGroupName { get; set; }
        public string ResourceProviderType { get; set; }
        public string ResourceType { get; set; }
        public string RegionName { get; set; }
        public decimal? cost { get; set; }
        public string Cloudprovider { get; set; }
        public string SubscriptionId { get; set; }
        public string currency { get; set; }
    }
}
