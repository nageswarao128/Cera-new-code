using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.Entities.ViewModels
{
    public class StorageContainerKeysMapDTO
    {
        public string storageAccountId { get; set; }
        public string storageAccountName { get; set; }
        public string containerName { get; set; }
        public string key { get; set; }
    }
}
