using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.Entities.ViewModels
{
    public class RegisterOrgModel
    {
        public string OrganizationName { get; set; }
        public string PrimaryAddress { get; set; }
        public string OrgDescription { get; set; }
        public string ContactPersonName { get; set; }
        public string EmailId { get; set; }
        public int PhoneNo { get; set; }
        public Guid UserId { get; set; }


        public string CloudProviderName { get; set; }
        public string DllPath { get; set; }
        public string FullyQualifiedClassName { get; set; }
        public DateTime DateEnabled { get; set; }
        public string DevContact { get; set; }
        public string SupportContact { get; set; }
        public string Description { get; set; }
        
        
        
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
