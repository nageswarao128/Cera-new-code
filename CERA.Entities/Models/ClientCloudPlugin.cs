using System.ComponentModel.DataAnnotations.Schema;

namespace CERA.Entities.Models
{
    [Table("tbl_ClientCloudPlugins")]
    public class ClientCloudPlugin : BaseEntity
    {
        public Client Client { get; set; }
        public CloudPlugIn PlugIn { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
