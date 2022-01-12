using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeraWebApplication.Models
{
    public class StorageSizes
    {
        public int? Id { get; set; }
        public string ResourceID { get; set; }
        public string Name { get; set; }
        public string StorageType { get; set; }
        public float? Size { get; set; }
    }
}
