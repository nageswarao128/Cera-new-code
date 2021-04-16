using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeraWebApplication.Models
{
    public class CeraUserModel
    { 
        public string Id { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string[] Roles { get; set; }
    }

}
