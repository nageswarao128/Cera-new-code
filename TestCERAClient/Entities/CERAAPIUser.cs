using Microsoft.AspNetCore.Identity;
using System;

namespace CERAAPI.Entities
{
    public class CERAAPIUser : IdentityUser
    {
        public string OrgName { get; set; }
        //public string Description { get; set; }
        //public DateTime DateCreated { get; set; } = DateTime.Now;
        //public DateTime DateUpdated { get; set; } = DateTime.Now;
        //public string UpdatedBy { get; set; } = "System";
        //public string CreatedBy { get; set; } = "System";
    }
    public class CERAAPIRole : IdentityRole
    {
        //public DateTime DateCreated { get; set; } = DateTime.Now;
        //public string CreatedBy { get; set; } = "System";
    }
}
