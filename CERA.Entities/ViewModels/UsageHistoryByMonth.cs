using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.Entities.ViewModels
{
    public class UsageHistoryByMonth
    {
        public string resourceType { get; set; }
        public decimal cost { get; set; }
        public string usageMonth { get; set; }

    }
}
