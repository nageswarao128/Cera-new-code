using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CERA.Entities
{
    [Table("tbl_Subscription")]
    public class SubscriptionList

    {
        public int ID { get; set; }
        public int BatchID { get; set; }
        public int StepID { get; set; }
        public DateTime? StartedAt { get; set; } = null;
        public DateTime? EndedAt { get; set; } = null;
        public string SubscriptionId { get; set; }
        public string TenantID { get; set; }
        public string SubscriptionName { get; set; }
        public string AuthorizationSource { get; set; }
    }
}
