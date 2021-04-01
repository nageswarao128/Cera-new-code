using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeraWebApplication.Models
{
    public class CeraTenants
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string TenantId { get; set; }
    }
}
