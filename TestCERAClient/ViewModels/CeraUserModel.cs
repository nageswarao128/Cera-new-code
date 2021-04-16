using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CERAAPI.ViewModels
{
    public class CeraUserModel
    { 
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string[] Role { get; set; }
    }
}
