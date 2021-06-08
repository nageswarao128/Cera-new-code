using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CERA.Entities.Models
{
    [Table("tbl_policy")]
    public class CeraPolicy
    {
        [Key]
        public int? Id { get; set; }
        public string PolicyId { get; set; }
        public string PrincipleName { get; set; }
        public string ResourceGroupName { get; set; }
        public string Key { get; set; }
        public string Scope { get; set; }
    }
}
