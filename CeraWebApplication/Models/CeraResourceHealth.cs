using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeraWebApplication.Models
{
    public class CeraResourceHealth
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string AvailabilityState { get; set; }
        public string Type { get; set; }
    }
}
