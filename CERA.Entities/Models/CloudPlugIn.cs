using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CERA.Entities.Models
{
    /// <summary>
    /// This class is used as a object for cloud plugin details
    /// </summary>
    [Table("tbl_CloudPlugIns")]
    public class CloudPlugIn : BaseEntity
    {
        public string CloudProviderName { get; set; }
        public string ServiceName { get; set; }
        public string AssemblyName { get; set; }
        public string ClassName { get; set; }
        public string DllPath { get; set; }
        public string FullyQualifiedName { get; set; }
        public DateTime DateEnabled { get; set; }
        public string DevContact { get; set; }
        public string SupportContact { get; set; }
        public string Description { get; set; }
    }
}
