using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeraWebApplication.Models
{
    public class CeraPolicies
    {
        public int Id { get; set; }
        public string PolicyId { get; set; }
        public string PrincipleName { get; set; }
        public string ResourceGroupName { get; set; }
        public string Key { get; set; }
        public string Scope { get; set; }
    }
}
