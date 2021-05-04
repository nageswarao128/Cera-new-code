using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.Entities.ViewModels
{
    public class ResourceHealthViewDTO
    {
        public string ResourceName { get; set; }
        public string ResourceGroupName { get; set; }
        public string ResourceType { get; set; }
        public string Location { get; set; }
        public string AvailabilityState { get; set; }
    }
}
