using System;

namespace CERAAPI.ViewModels
{
    public class AddPlatformViewModel
    {
        public Guid OrganizationId { get; set; }
        public string PlatformName { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
