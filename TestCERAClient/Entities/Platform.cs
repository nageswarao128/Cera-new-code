namespace CERAAPI.Entities
{
    public class Platform
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string PlatformName { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
