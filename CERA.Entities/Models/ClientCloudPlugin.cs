using System.ComponentModel.DataAnnotations.Schema;

namespace CERA.Entities.Models
{
    /// <summary>
    /// This class is used as a object for client cloud plugin details
    /// </summary>
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
