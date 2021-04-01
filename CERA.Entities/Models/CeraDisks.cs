using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CERA.Entities.Models
{
    /// <summary>
    /// This class is used as a object for Disks
    /// </summary>
    [Table("tbl_Disks")]
    public class CeraDisks
    {
        [Key]
        public int? ID { get; set; }
        public string Name { get; set; }
        public string RegionName { get; set; }
        public string ResourceGroupName { get; set; }
    }
}
