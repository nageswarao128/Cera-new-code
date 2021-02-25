using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CERAAPI.Entities
{
    [Table("tbl_CloudPlugIns")]
    public class CloudPlugIn : BaseEntity
    {
        public string CloudProviderName { get; set; }
        public string AssemblyName { get; set; }
        public string DllPath { get; set; }
        public string FullyQualifiedName { get; set; }
        public DateTime DateEnabled { get; set; }
        public string DevContact { get; set; }
        public string SupportContact { get; set; }
        public string Description { get; set; }
    }
}
