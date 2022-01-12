using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.Entities
{
    public class ViewModelBase
    {
        public string token { get; set; }
        public string tenantId { get; set; }
        public string clientId { get; set; }
        public string clientSecret { get; set; }
    }
}
