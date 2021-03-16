using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CERA.Entities.Models
{
    /// <summary>
    /// This class is used as a object for ResourceGroups
    /// </summary>
    [Table("tbl_ResourceGroups")]
    public class CeraResourceGroups
    {
        [Key]
        public int? Id { get; set; }
        public string Name { get; set; }
        public string RegionName { get; set; }
        public string provisioningstate { get; set; }
       
    }
}
