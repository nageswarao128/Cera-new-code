using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CERA.Entities
{
    [Table("tbl_Subscription")]
    public class CeraSubscription
    {
        [Key]
        public string SubscriptionId { get; set; }
        public string DisplayName { get; set; }
        public string TenantID { get; set; }
    }
}
