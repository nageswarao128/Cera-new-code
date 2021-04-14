using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.AuthenticationService
{
    public class UpdateUserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string[] Role { get; set; }
    }
}
