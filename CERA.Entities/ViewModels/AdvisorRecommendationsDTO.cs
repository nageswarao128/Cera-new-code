using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.Entities.ViewModels
{
    public class AdvisorRecommendationsDTO
    {
        [JsonProperty(PropertyName = "properties")]
        public AdvisorProperties properties { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
    }
    public class AdvisorProperties
    {
        public string category { get; set; }
        public string impact { get; set; }
        public string impactedField { get; set; }
        public string impactedValue { get; set; }
        public DateTime lastUpdated { get; set; }
        public string recommendationTypeId { get; set; }
        public Shortdescription shortDescription { get; set; }
        public Resourcemetadata resourceMetadata { get; set; }
        public Extendedproperties extendedProperties { get; set; }
    }

    public class Shortdescription
    {
        public string problem { get; set; }
        public string solution { get; set; }
    }

    public class Resourcemetadata
    {
        public string resourceId { get; set; }
    }

    public class Extendedproperties
    {
        public string reservedResourceType { get; set; }
        public string subId { get; set; }
        public string scope { get; set; }
        public string sku { get; set; }
        public string term { get; set; }
        public string annualSavingsAmount { get; set; }
        public string savingsAmount { get; set; }
        public string qty { get; set; }
        public string savingsCurrency { get; set; }
        public string region { get; set; }
        public string displayQty { get; set; }
        public string displayRegion { get; set; }
        public string displaySKU { get; set; }
        public string location { get; set; }
        public string vmSize { get; set; }
        public string targetResourceCount { get; set; }
        public string savingsPercentage { get; set; }
        public string reservationType { get; set; }
    }


}
