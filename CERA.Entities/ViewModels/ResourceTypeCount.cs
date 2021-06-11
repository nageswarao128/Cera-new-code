using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CERA.Entities.ViewModels
{
    public class ResourceTypeCount
    {
        
        public string ResourceType { get; set; }
        public int ResourceCount { get; set; }
        public string Color { get; set; }
        public decimal ConsumedPercent { get; set; }
    }
}
