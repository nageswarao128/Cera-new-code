using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CERA.Entities.Models
{
    [Table("tbl_UsageDetails")]
    public class CeraUsage
    {
        [Key]
        public int? UsageId { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
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
