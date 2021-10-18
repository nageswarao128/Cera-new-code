using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.Entities.ViewModels
{
    public class UsageByResourceGroup
    {
        public string instanceResourceGroup { get; set; }
        public decimal ConsumedCost { get; set; }
    }
}
