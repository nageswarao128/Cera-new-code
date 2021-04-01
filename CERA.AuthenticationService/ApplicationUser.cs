using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.AuthenticationService
{
    public class ApplicationUser:IdentityUser
    {
        public string OrgName { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime UpdatedTime { get; set; } = DateTime.Now;
    }
    public class ApplicationRoles : IdentityRole
    {
    }
}
