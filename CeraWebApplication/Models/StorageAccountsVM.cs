using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeraWebApplication.Models
{
    public class StorageAccountsVM
    {
        public string StorageAccountId { get; set; }
        public string Name { get; set; }
        //public float? blobSize { get; set; }
        public string RegionName { get; set; }
        public string ResourceGroupName { get; set; }
        public string type { get; set; }
        public decimal? cost { get; set; }
        public string SubscriptionId { get; set; }
        public string CloudProvider { get; set; }

    }
    
}
