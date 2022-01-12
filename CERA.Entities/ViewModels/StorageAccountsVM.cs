using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.Entities.ViewModels
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
