using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeraWebApplication.Models
{
    public class CeraResourceGroup:UtilityModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RegionName { get; set; }
        public string provisioningstate { get; set; }
        public string Resourcegroupid { get; set; }
    }
}
