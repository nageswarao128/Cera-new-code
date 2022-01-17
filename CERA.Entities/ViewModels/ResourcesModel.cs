using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.Entities.ViewModels
{
    public class ResourcesModel
    {
        public string Name { get; set; }
        public string RegionName { get; set; }
        public string ResourceGroupName { get; set; }
        public string type { get; set; }
        public decimal? cost { get; set; }
        public string SubscriptionId { get; set; }
        public string CloudProvider { get; set; }
        public string currency { get; set; }
    }
}
