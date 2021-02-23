using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.Entities
{
   public class ManagePlatform
    {
        public int id { get; set; }
        public int orgId { get; set; }
        public string platformName { get; set; }
        public string tenantId { get; set; }
        public string clientId { get; set; }
        public string clientSecret { get; set; }
    }
}
