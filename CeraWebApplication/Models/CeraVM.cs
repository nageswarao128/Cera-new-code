using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeraWebApplication.Models
{
    public class CeraVM
    {
        public int VMId { get; set; }
        public string VMName { get; set; }
        public string RegionName { get; set; }
        public string ResourceGroupName { get; set; }
    }
}
