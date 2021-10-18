using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeraWebApplication.Models
{
    public class PolicyRules
    {
        public Guid id { get; set; }
        public string subscriptionName { get; set; }
        public string policyRule { get; set; }
    }
}
