using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.AuthenticationService
{
    public class RegisterUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailID { get; set; }
        public string OrgName { get; set; }
        public string Role { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }

   
}
