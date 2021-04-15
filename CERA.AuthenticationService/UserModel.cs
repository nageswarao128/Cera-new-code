using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.AuthenticationService
{
    public class UserModel
    {
        public string id { get; set; }
        public string userName { get; set; }
        public string emailId { get; set; }
        public IList<string> roles { get; set; }
    }
    
}
