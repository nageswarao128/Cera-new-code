using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.Entities.Models
{
    public class StorageAccountsSizeVM
    {
        public string storageAccountId { get; set; }
        public string name { get; set; }
        public string resourceGroupName { get; set; }
        public string region { get; set; }
        public string type { get; set; }
        public List<StorageSizeVM> storageSizes { get; set; }
        public decimal? cost { get; set; }
        public string cloudProvider { get; set; }
        public string subscriptionId { get; set; }
    }
    public class StorageSizeVM
    {
        public string storageType { get; set; }
        public float? size { get; set; }
    }
}
