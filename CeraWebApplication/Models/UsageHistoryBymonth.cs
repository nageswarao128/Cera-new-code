using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeraWebApplication.Models
{
    public class UsageHistoryBymonth
    {
        public string resourceType { get; set; }
        public decimal cost { get; set; }
        public string usageMonth { get; set; }
    }
}
