using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CERA.Entities.Models
{
    /// <summary>
    /// This class is used as a object for Resources
    /// </summary>
    [Table("tbl_Resources")]
    public class CeraResources
    {   
        [Key]
        public int? ResourceID { get; set; }
        public string Id { get; set; }
        public string ResourceProviderNameSpace { get; set; }
        public string Name { get; set; }
        public string RegionName { get; set; }
        public string ResourceGroupName { get; set; }
        public string ResourceType { get; set; }
        public bool Tags { get; set; }
    }
}
