using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.Entities.ViewModels
{
    public class StorageAccountKeysDTO
    {

        public DateTime creationTime { get; set; }
        public string keyName { get; set; }
        public string value { get; set; }
        public string permissions { get; set; }
    }

}

