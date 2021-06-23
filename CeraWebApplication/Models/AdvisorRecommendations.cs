using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeraWebApplication.Models
{
    public class AdvisorRecommendations
    {
        public int id { get; set; }
        public string recommendationId { get; set; }
        public string category { get; set; }
        public string impact { get; set; }
        public string resourceId { get; set; }
        public string location { get; set; }
    }
}
