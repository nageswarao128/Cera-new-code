using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.CloudService.CERAEntities
{
    public class CeraSubscriptionList
    {
        public string SubscriptionId { get; set; }
        public string DisplayName { get; set; }
        public string TenantID { get; set; }
        public string AuthorizationSource { get; set; }

    }
}
