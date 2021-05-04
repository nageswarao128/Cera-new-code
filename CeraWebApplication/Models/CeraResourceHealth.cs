using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeraWebApplication.Models
{
    public class CeraResourceHealth
    {
        public string ResourceName { get; set; }
        public string ResourceGroupName { get; set; }
        public string ResourceType { get; set; }
        public string Location { get; set; }
        public string AvailabilityState { get; set; }
    }
}
