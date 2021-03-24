using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CERA.Entities.Models
{
    /// <summary>
    /// This class is used as a object for WebApps
    /// </summary>
    [Table("tbl_WebApps")]
    public class CeraWebApps
    {
        [Key]
        public int? Id { get; set; }
        public string Name { get; set; }
        public string RegionName { get; set; }
        public string ResourceGroupName { get; set; }
    }
}
