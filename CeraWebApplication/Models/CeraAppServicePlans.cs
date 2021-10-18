﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeraWebApplication.Models
{
    public class CeraAppServicePlans:UtilityModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string RegionName { get; set; }
        public string ResourceGroupName { get; set; }
        public string AppServicePlanId { get; set; }
    }
}
