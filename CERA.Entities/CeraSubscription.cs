using System.ComponentModel.DataAnnotations;

namespace CERA.Entities
{
    public class CeraSubscription
    {
        [Key]
        public string SubscriptionId { get; set; }
        public string DisplayName { get; set; }
        public string TenantID { get; set; }
    }
}
