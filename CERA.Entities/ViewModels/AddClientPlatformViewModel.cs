using System;

namespace CERA.Entities.ViewModels
{
    /// <summary>
    /// This class is used as a data transfer objecct for adding a client platform
    /// </summary>
    public class AddClientPlatformViewModel
    {
        public string OrganizationName { get; set; }
        public Guid OrganizationId { get; set; }
        public string PlatformName { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
