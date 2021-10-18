using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.Entities.ViewModels
{
    public class BillingDTO
    {
        public string nextLink { get; set; }
        [JsonProperty(PropertyName = "Value")]
        public Values[] value { get; set; }
    }

    public class Values
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        [JsonProperty(PropertyName = "Tags")]
        public BillingTags tags { get; set; }
        [JsonProperty(PropertyName = "properties")]
        public BillingProperties properties { get; set; }
    }

    public class BillingTags
    {
        public string msresourceusage { get; set; }
        public string env { get; set; }
    }

    public class BillingProperties
    {
        public string billingPeriodId { get; set; }
        public DateTime usageStart { get; set; }
        public DateTime usageEnd { get; set; }
        public string instanceId { get; set; }
        public string instanceName { get; set; }
        public string instanceLocation { get; set; }
        public string meterId { get; set; }
        public decimal usageQuantity { get; set; }
        public decimal pretaxCost { get; set; }
        public string currency { get; set; }
        public bool isEstimated { get; set; }
        public string subscriptionGuid { get; set; }
        public string subscriptionName { get; set; }
        public string product { get; set; }
        public string consumedService { get; set; }
        public string partNumber { get; set; }
        public string resourceGuid { get; set; }
        public string offerId { get; set; }
        public bool chargesBilledSeparately { get; set; }
        public string meterDetails { get; set; }
        public string additionalProperties { get; set; }
    }

}
